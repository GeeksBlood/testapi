using Aguila.DAL;
using Aguila.Models;
using Aguila.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aguila.Service
{
    /// <summary>
    /// This class have interface methods implementation of ITokenService
    /// for validating the user.
    /// </summary>
    public class TokenService : ITokenService
    {
        public async Task<UserInfoDto> ValidateUser(string username, string password)
        {
            try
            {
                using (AguilaContext _context = new AguilaContext())
                {
                    var users = await (from user in _context.AspNetUsers
                                       join roles in _context.AspNetUserRoles on user.Id equals roles.UserId
                                       join roleType in _context.AspNetRoles on roles.RoleId equals roleType.Id
                                       where user.UserName == username && user.PasswordHash == password
                                       select new UserInfoDto
                                       {
                                           UserId = user.Id,
                                           UserName = user.UserName,
                                           Name = user.Name,
                                           Role = roleType.Name
                                       }).FirstOrDefaultAsync();
                    return users;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
