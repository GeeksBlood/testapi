using Aguila.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aguila.Service.Interface
{
    /// <summary>
    /// Interface for the Answer service class.
    /// </summary>
    public interface IAnswerService
    {
        Task<IEnumerable<AnswersDto>> GetAll();
        Task<AnswersDto> GetByID(int id);
        Task<bool> Insert(AnswersDto data);
        Task<bool> Update(int id, AnswersDto data);
        Task<bool> Delete(int id);
    }
}
