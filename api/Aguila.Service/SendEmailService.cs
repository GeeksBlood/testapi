using Aguila.Service.Interface;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Aguila.Service
{
    public class SendEmailService: ISendEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnv;
        public SendEmailService(IConfiguration configuration, IHostingEnvironment hostingEnv)
        {
            this._configuration = configuration;
            this._hostingEnv = hostingEnv;
        }
        public async Task<bool> SendingEmailAsync(string Name, string EmailAddress)
        {
            try
            {
                string messageText = string.Empty;
                MimePart attachment = new MimePart();

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_configuration.GetSection("Settings").GetSection("fromAdressTitle").Value, _configuration.GetSection("Settings").GetSection("smtpFromAddress").Value));
                message.To.Add(new MailboxAddress(_configuration.GetSection("Settings").GetSection("toAdressTitle").Value, _configuration.GetSection("Settings").GetSection("toRegisterUser").Value));
                if (!string.IsNullOrEmpty(_configuration.GetSection("Settings").GetSection("toRegisterUser2").Value))
                {
                    message.To.Add(new MailboxAddress(_configuration.GetSection("Settings").GetSection("toAdressTitle").Value, _configuration.GetSection("Settings").GetSection("toRegisterUser2").Value));
                }
                if (!string.IsNullOrEmpty(_configuration.GetSection("Settings").GetSection("ccRegisterUser").Value))
                {
                    message.Cc.Add(new MailboxAddress(_configuration.GetSection("Settings").GetSection("toAdressTitle").Value, _configuration.GetSection("Settings").GetSection("ccRegisterUser").Value));
                }
                if (!string.IsNullOrEmpty(_configuration.GetSection("Settings").GetSection("bccRegisterUser").Value))
                {
                    message.Bcc.Add(new MailboxAddress(_configuration.GetSection("Settings").GetSection("toAdressTitle").Value, _configuration.GetSection("Settings").GetSection("bccRegisterUser").Value));
                }
                message.Subject = "New User Registeration";

                attachment.ContentObject = new ContentObject(File.OpenRead(_hostingEnv.WebRootPath + "/Images/aguila-golf-logo.png"), ContentEncoding.Default);
                attachment.ContentDisposition = new ContentDisposition(ContentDisposition.Inline);
                attachment.ContentId = "logo.png";
                attachment.ContentType.MediaType = "image/png";
                attachment.ContentType.Name = "logo.png";

                messageText = File.ReadAllText(_hostingEnv.WebRootPath + "/EmailLayout/SignupEmail.html");
                messageText = messageText.Replace("@@NamePlaceHolder", Name);
                messageText = messageText.Replace("@@EmailPlaceHolder", EmailAddress);

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
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
