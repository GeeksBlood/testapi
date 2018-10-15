using Aguila.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aguila.Service.Interface
{
    /// <summary>
    /// Interface for the Attribute service class.
    /// </summary>
    public interface IAttributeService
    {
        Task<IEnumerable<AttributesDto>> GetAll();
        Task<AttributesDto> GetByID(int id);
        Task<IEnumerable<AttributeInformation>> GetAttributesByCategory(int Categoryid);
        Task<bool> Insert(AttributesDto data);
        Task<bool> Update(int id, AttributesDto data);
        Task<bool> Delete(int id);
    }
}
