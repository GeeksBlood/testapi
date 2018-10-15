using Aguila.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aguila.Service.Interface
{
    /// <summary>
    /// Interface for the Situation service class.
    /// </summary>
    public interface ISituationService
    {
        Task<IEnumerable<SituationsDto>> GetSituation();
        Task<IEnumerable<SituationsByHoleDto>> GetSituationByHole();
        Task<IEnumerable<SituationsDto>> GetAllSituation();
        Task<IEnumerable<SituationHoleDto>> GetSituationIsFirstHole();
        Task<SituationsDto> GetSituationByID(int id);
        Task<IEnumerable<SituationsDto>> GetSituationsByCHA(int C, int[] H, int[] A);
        Task<IEnumerable<SituationCategoriesDto>> GetSituationCategories();
        Task<EditSituationViewModel> UpdateSituation(int id, EditSituationViewModel data);
        Task<NewSituationViewDto> CreateSituation(NewSituationViewDto data);
        Task<bool> DeleteSituation(int id);
        Task<SituationsDto> PublishSituation(int id);
        Task<SituationsDto> UnPublishSituation(int id);
        Task<IEnumerable<SituationsDto>> GetSituationsByNextHoleId(int id);

    }
}
