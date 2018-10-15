using Aguila.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aguila.Service.Interface
{
    /// <summary>
    /// Interface for the Picture Book service class.
    /// </summary>
    public interface IPictureBookService
    {
        Task<IEnumerable<PictureBooksDto>> GetAll();
        Task<IEnumerable<PictureBooksAdminDto>> GetAllByAdmin();
        Task<PictureBooksDto> GetByID(int id);
        Task<PictureBooksDto> Insert(PictureBooksDto data);
        Task<bool> Update(int id, PictureBooksDto data);
        Task<bool> Delete(int id);
    }
}
