using Aguila.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aguila.Service.Interface
{
    /// <summary>
    /// Interface for the Handicap service class.
    /// </summary>
    public interface IHandicapService
    {
        Task<IEnumerable<HandicapsDto>> GetAll();
        Task<HandicapsDto> GetByID(int id);
        Task<bool> Insert(HandicapsDto data);
        Task<bool> Update(int id, HandicapsDto data);
        Task<bool> Delete(int id);
    }
}
