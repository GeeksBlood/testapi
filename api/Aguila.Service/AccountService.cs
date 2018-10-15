using Aguila.DAL;
using Aguila.Models;
using Aguila.Models.AccountModel;
using Aguila.Service.Interface;
using AutoMapper;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using MimeKit;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Aguila.Service
{
    /// <summary>
    /// This class have interface methods implementation of IAccountService
    /// for Change Password, Forgot Password and Logout.
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnv;
        private readonly IConfiguration _configuration;
        private readonly AguilaContext _aguilaContext;
        private readonly string _externalCookieScheme;
        private readonly RoleManager<ApplicationRoleDto> _roleManager;
        private readonly UserManager<ApplicationUserDto> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<ApplicationUserDto> _signInManager;
        private readonly IPasswordHasher<ApplicationUserDto> _passwordHasher;

        public AccountService(IMapper mapper
                                , AguilaContext aguilaContext
                                , ILoggerFactory loggerFactory
                                , IConfiguration configuration
                                , IHostingEnvironment hostingEnv
                                , RoleManager<ApplicationRoleDto> roleManager
                                , UserManager<ApplicationUserDto> userManager
                                , IHttpContextAccessor httpContextAccessor
                                , SignInManager<ApplicationUserDto> signInManager
                                , IOptions<IdentityCookieOptions> identityCookieOptions
                                , IPasswordHasher<ApplicationUserDto> passwordHasher
                             )
        {
            _mapper = mapper;
            _hostingEnv = hostingEnv;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _aguilaContext = aguilaContext;
            _configuration = configuration;
            this._httpContextAccessor = httpContextAccessor;
            _externalCookieScheme = identityCookieOptions.Value.ExternalCookieAuthenticationScheme;
            _passwordHasher = passwordHasher;
        }

        /// <summary>
        /// This method allows to change user's current password. 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> ChangePassword(ChangePasswordBindingModel model)
        {
            var users = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);

            IdentityResult result = await _userManager.ChangePasswordAsync(users, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Logout the current user from the application.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Logout()
        {
            await _signInManager.SignOutAsync();
            return true;
        }


        /// <summary>
        /// This method composes mail with attachment and link inside it to Reset Password,
        /// and sends to appropriate user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> ForgotPassword(ForgotPasswordViewModel model)
        {
            try
            {
                string randomPWD = string.Empty;
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user != null)
                {
                    var random = new Random();
                    randomPWD = "AGolfVR@" + new string(Enumerable.Repeat("1234567890", 4).Select(s => s[random.Next(s.Length)]).ToArray());
                    user.PasswordHash = _passwordHasher.HashPassword(user, randomPWD);

                    await _userManager.UpdateSecurityStampAsync(user);

                    //save changes
                    IdentityResult v = await _userManager.UpdateAsync(user);

                    string messageText, code, baseURL, resetURL = string.Empty;
                    MimePart attachment = new MimePart();
                    code = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress(_configuration.GetSection("Settings").GetSection("fromAdressTitle").Value, _configuration.GetSection("Settings").GetSection("smtpFromAddress").Value));
                    message.To.Add(new MailboxAddress(_configuration.GetSection("Settings").GetSection("toAdressTitle").Value, model.Email));
                    message.Subject = _configuration.GetSection("Settings").GetSection("subject").Value;

                    baseURL = _configuration.GetSection("appSettings").GetSection("AdminBaseUrl").Value;
                    resetURL = string.Format("{0}/forgotPassword?token={1}&Email={2}", baseURL, WebUtility.UrlEncode(code), WebUtility.UrlEncode(model.Email));

                    if (File.Exists(_hostingEnv.ContentRootPath + "/App_Data/EmailBody.html"))
                    {
                        // create an image attachment for the file located at path
                        attachment.ContentObject = new ContentObject(File.OpenRead(_hostingEnv.ContentRootPath + "/Images/aguila-golf-logo.png"), ContentEncoding.Default);
                        attachment.ContentDisposition = new ContentDisposition(ContentDisposition.Inline);
                        attachment.ContentId = "logo.png";
                        attachment.ContentType.MediaType = "image/png";
                        attachment.ContentType.Name = "logo.png";

                        messageText = File.ReadAllText(_hostingEnv.ContentRootPath + "/App_Data/EmailBody.html");
                        //messageText = messageText.Replace("@@URLPlaceHolder", resetURL);
                        messageText = messageText.Replace("@@PWDPlaceHolder", randomPWD);
                    }
                    else
                    {
                        //var file = System.IO.Path.Combine(_hostingEnv.ContentRootPath, "/App_Data/EmailBody.html");
                        attachment.ContentObject = new ContentObject(File.OpenRead(_hostingEnv.WebRootPath + "/Images/aguila-golf-logo.png"), ContentEncoding.Default);
                        attachment.ContentDisposition = new ContentDisposition(ContentDisposition.Inline);
                        attachment.ContentId = "logo.png";
                        attachment.ContentType.MediaType = "image/png";
                        attachment.ContentType.Name = "logo.png";

                        messageText = File.ReadAllText(_hostingEnv.WebRootPath + "/EmailLayout/EmailBody.html");
                        //messageText = messageText.Replace("@@URLPlaceHolder", resetURL);
                        messageText = messageText.Replace("@@PWDPlaceHolder", randomPWD);
                        //messageText = _configuration.GetSection("Settings").GetSection("body").Value.Replace("@@URLPlaceHolder", resetURL);
                    }

                    // Create our message text, just like before (except don't set it as the message.Body)
                    var body = new TextPart("html")
                    {
                        Text = messageText
                    };

                    // Create the multipart/mixed container to hold the message text and
                    // the image attachment
                    var multipart = new Multipart("mixed");
                    multipart.Add(body);
                    multipart.Add(attachment);

                    // Set the multipart/mixed as the message body
                    message.Body = multipart;

                    using (var client = new SmtpClient())
                    {
                        await client.ConnectAsync(_configuration.GetSection("Settings").GetSection("smtpClient").Value
                                                 , Convert.ToInt32(587)
                                                 , SecureSocketOptions.None).ConfigureAwait(false);
                        client.Authenticate(_configuration.GetSection("Settings").GetSection("smtpClientUsername").Value
                                                 , _configuration.GetSection("Settings").GetSection("smtpClientPassword").Value);
                        await client.SendAsync(message).ConfigureAwait(false);
                        await client.DisconnectAsync(true).ConfigureAwait(false);
                    }

                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Blob storage for android and IOS assets bundle
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAccountSASToken()
        {
            string ConnectionString = _configuration.GetConnectionString("BlobStorage");
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);

            // Create a new access policy for the account.
            SharedAccessAccountPolicy policy = new SharedAccessAccountPolicy()
            {
                Permissions = SharedAccessAccountPermissions.Read | SharedAccessAccountPermissions.Write | SharedAccessAccountPermissions.List,
                Services = SharedAccessAccountServices.Blob | SharedAccessAccountServices.File,
                ResourceTypes = SharedAccessAccountResourceTypes.Object,//.Container,//.Service,  // object is a file to upload
                SharedAccessExpiryTime = DateTime.UtcNow.AddYears(20),
                Protocols = SharedAccessProtocol.HttpsOnly
            };
            return await Task.FromResult<string>(storageAccount.GetSharedAccessSignature(policy));
        }
    }
}
