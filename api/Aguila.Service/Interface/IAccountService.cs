using Aguila.Models;
using Aguila.Models.AccountModel;
using System.Threading.Tasks;

namespace Aguila.Service.Interface
{
    /// <summary>
    /// Interface for the Account Service class.
    /// </summary>
    public interface IAccountService
    {
        Task<bool> ChangePassword(ChangePasswordBindingModel model);
        Task<bool> Logout();
        Task<bool> ForgotPassword(ForgotPasswordViewModel model);
        Task<string> GetAccountSASToken();
    }
}
