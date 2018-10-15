using Aguila.Models;
using System.Threading.Tasks;

namespace Aguila.Service.Interface
{
    /// <summary>
    /// Interface for the Token service class.
    /// </summary>
    public interface ITokenService
    {
        Task<UserInfoDto> ValidateUser(string username, string password);
    }
}
