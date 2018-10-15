using Aguila.Models;
using System.Threading.Tasks;

namespace Aguila.Service.Interface
{
    /// <summary>
    /// Interface for User Controller class for
    /// Insert, Get, Update and Delete operations.
    /// </summary>
    public interface IUserService
    {
        Task<UserDto> GetById(string id);
        Task<bool> Insert(ApplicationUserDto data);
        Task<SuccessDto> RegisterByApp(RegisterAppUserDto model);
        Task<bool> Update(string id, ApplicationUserDto data);
        Task<UserInfoDto> GeteUserByUsername(string username);
        Task<ApplicationUserDto> Delete(string id);
    }
}
