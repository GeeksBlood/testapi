using System.Threading.Tasks;

namespace Aguila.Service.Interface
{
    public interface ISendEmailService
    {
        Task<bool> SendingEmailAsync(string Name, string EmailAddress);
    }
}
