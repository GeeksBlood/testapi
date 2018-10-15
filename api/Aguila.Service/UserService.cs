using Aguila.DAL;
using Aguila.Models;
using Aguila.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using Aguila.Common;

namespace Aguila.Service
{
    /// <summary>
    /// User service class to implement IUserService interface methods.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly AguilaContext _aguilaContext;
        private readonly string _externalCookieScheme;
        private readonly RoleManager<ApplicationRoleDto> _roleManager;
        private readonly UserManager<ApplicationUserDto> _userManager;
        private readonly SignInManager<ApplicationUserDto> _signInManager;
        private readonly ISendEmailService _sendEmailService;
        public UserService(IMapper mapper
                             , ILoggerFactory loggerFactory
                             , AguilaContext aguilaContext
                             , RoleManager<ApplicationRoleDto> roleManager
                             , UserManager<ApplicationUserDto> userManager
                             , SignInManager<ApplicationUserDto> signInManager
                             , IOptions<IdentityCookieOptions> identityCookieOptions
                             , ISendEmailService sendEmailService
                          )
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _aguilaContext = aguilaContext;
            _externalCookieScheme = identityCookieOptions.Value.ExternalCookieAuthenticationScheme;
            _sendEmailService = sendEmailService;
        }

        /// <summary>
        /// Get the User Details by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserDto> GetById(string id)
        {
            var users = await _aguilaContext.AspNetUsers.Where(u => u.Id == id).FirstOrDefaultAsync();

            if (users != null)
            {
                return _mapper.Map<AspNetUsers, UserDto>(users);
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// Create user and save its details,
        /// sign it and assign User role to him/her.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Insert(ApplicationUserDto model)
        {
            try
            {
                if (model.VoiceOver == null)
                {
                    model.VoiceOver = false;
                }

                if (model.BallFlight == null)
                {
                    model.BallFlight = "Multiple";
                }

                if (model.HandicapId < 1 || model.HandicapId > 4)
                {
                    model.HandicapId = 4;
                }


                var user = new ApplicationUserDto
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Name = model.Name,
                    Address = model.Address,
                    City = model.City,
                    State = model.State,
                    Zip = model.Zip,
                    DatePaid = model.DatePaid,
                    Twitter = model.Twitter,
                    PhoneNumber = model.PhoneNumber,
                    LockoutEndDateUtc = model.LockoutEndDateUtc,
                    ClubType = model.ClubType,
                    Height = model.Height,
                    Weight = model.Weight,
                    Age = model.Age,
                    SevenIronDistance = model.SevenIronDistance,
                    DriverDistance = model.DriverDistance,
                    VoiceOver = model.VoiceOver,
                    BallFlight = model.BallFlight,

                    // Can not be null.
                    AccountActive = model.AccountActive,
                    EmailConfirmed = model.EmailConfirmed,
                    PhoneNumberConfirmed = model.PhoneNumberConfirmed,
                    TwoFactorEnabled = model.PhoneNumberConfirmed,
                    LockoutEnabled = model.LockoutEnabled,
                    AccessFailedCount = model.AccessFailedCount,
                    HandicapId = model.HandicapId,
                };

                // Create the new user
                var result = await _userManager.CreateAsync(user, model.PasswordHash);

                if (result.Succeeded)
                {
                    ApplicationRoleDto applicationRole = await _roleManager.FindByNameAsync(Common.Common.Roles.User.ToString());
                    if (applicationRole != null)
                    {
                        // Assign role to user.
                        IdentityResult roleResult = await _userManager.AddToRoleAsync(user, applicationRole.Name);
                        if (roleResult.Succeeded)
                        {
                            return true;
                        }
                        return false;
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// Create user and save its details by App,
        /// sign it and assign User role to him/her.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<SuccessDto> RegisterByApp(RegisterAppUserDto model)
        {
            SuccessDto obj = new SuccessDto();
            try
            {
                if (!string.IsNullOrEmpty(model.Name) && !string.IsNullOrEmpty(model.EmailID))
                {
                    obj.Success = await _sendEmailService.SendingEmailAsync(model.Name, model.EmailID);

                }
                return obj;
            }
            catch (Exception)
            {
                obj.Success= false;
                return obj;
            }
        }

        /// <summary>
        /// Update the user details if user exist.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Update(string id, ApplicationUserDto model)
        {
            try
            {
                if (model.VoiceOver == null)
                {
                    model.VoiceOver = false;
                }

                if (model.BallFlight == null)
                {
                    model.BallFlight = "Multiple";
                }

                if (model.HandicapId < 1 || model.HandicapId > 4)
                {
                    model.HandicapId = 4;
                }

                if (_aguilaContext.AspNetUsers.Count(e => e.Id == id) > 0)
                {
                    AspNetUsers modUsers = _aguilaContext.AspNetUsers.Where(u => u.Id == id).Select(u => u).FirstOrDefault();
                    modUsers.Name = model.Name;
                    modUsers.Address = model.Address;
                    modUsers.City = model.City;
                    modUsers.State = model.State;
                    modUsers.Zip = model.Zip;
                    modUsers.ClubType = model.ClubType;
                    modUsers.Email = model.Email;
                    modUsers.HandicapId = model.HandicapId;
                    modUsers.Height = model.Height;
                    modUsers.PhoneNumber = model.PhoneNumber;
                    modUsers.Twitter = model.Twitter;
                    modUsers.Weight = model.Weight;
                    modUsers.Age = model.Age;
                    modUsers.SevenIronDistance = model.SevenIronDistance;

                    modUsers.DriverDistance = model.DriverDistance;
                    modUsers.VoiceOver = model.VoiceOver;
                    modUsers.BallFlight = model.BallFlight;


                    await _aguilaContext.SaveChangesAsync();
                }
                else
                {
                    return false;
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return true;
        }

        /// <summary>
        /// Authenticate the user on the application.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<UserInfoDto> GeteUserByUsername(string username)
        {
            var userDetails = await _userManager.FindByNameAsync(username);
            if (userDetails != null)
            {
                var users = await (from user in _aguilaContext.AspNetUsers
                                   join roles in _aguilaContext.AspNetUserRoles on user.Id equals roles.UserId
                                   join roleType in _aguilaContext.AspNetRoles on roles.RoleId equals roleType.Id
                                   where user.UserName == username
                                   select new UserInfoDto
                                   {
                                       UserId = user.Id,
                                       UserName = user.UserName,
                                       Name = user.Name,
                                       Role = roleType.Name
                                   }).FirstOrDefaultAsync();

                await _userManager.AddToRoleAsync(userDetails, users.Role);
                return users;
            }

            return null;
        }

        /// <summary>
        /// Remove user from this application if it registered.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApplicationUserDto> Delete(string id)
        {
            try
            {
                ApplicationUserDto applicationUser = await _userManager.FindByIdAsync(id);
                if (applicationUser != null)
                {
                    AspNetUsers user = _aguilaContext.AspNetUsers.Where(x => x.Id == id).FirstOrDefault();
                    IdentityResult result = await _userManager.DeleteAsync(applicationUser);
                    if (result.Succeeded)
                    {
                        return _mapper.Map<AspNetUsers, ApplicationUserDto>(user);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
