using Aguila.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aguila.Service.Interface
{
    /// <summary>
    /// Interface for the LRD service class.
    /// </summary>
    public interface ILRDService
    {
        Task<IEnumerable<LrddatasDto>> GetLRDData();
        Task<IEnumerable<LrddatasDto>> GetLessonsData();
        Task<IEnumerable<LrddatasDto>> GetRulesData();
        Task<IEnumerable<LrddatasDto>> GetDidData();
        Task<IEnumerable<LrdcategoriesDto>> GetLRDCategory();
        Task<IEnumerable<LrdattributesDto>> GetLRDAttribute();
        Task<IEnumerable<LrdattributesDto>> GetLRDLessonsAttribute();
        Task<IEnumerable<LrdattributesDto>> GetLRDRulesAttribute();
        Task<LrddatasDto> Get(int id);
        Task<bool> UpdateLRD(int id, LrddatasDto data);
        Task<LrddatasDto> CreateLRD(LrddatasDto data);
        Task<LrddatasDto> DeleteLRD(int id);
    }
}
