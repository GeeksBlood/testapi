using Aguila.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aguila.Service.Interface
{
    /// <summary>
    /// Interface for the Configurations Values service class.
    /// </summary>
    public interface IConfigurationsValuesService
    {
        Task<IEnumerable<ConfigurationsValuesDto>> GetAll();
        Task<ConfigurationsValuesDto> GetByID(int id);
        Task<bool> Insert(ConfigurationsValuesDto data);
        Task<bool> Update(int id, ConfigurationsValuesDto data);
        Task<bool> Delete(int id);
    }
}
