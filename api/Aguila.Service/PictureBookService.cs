using Aguila.DAL;
using Aguila.Models;
using Aguila.Repository.Interface;
using Aguila.Service.Interface;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aguila.Service
{
    /// <summary>
    /// This class have interface methods implementation of IPictureBookService
    /// for Get, Get by ID, Create, Update and Delete PictureBook.
    /// </summary>
    public class PictureBookService : IPictureBookService
    {
        private readonly AguilaContext _context;
        private readonly IUnitOfWork _repo;
        private readonly IMapper _mapper;
        public PictureBookService(IUnitOfWork repo, IMapper mapper, AguilaContext context)
        {
            this._context = context;
            this._repo = repo;
            this._mapper = mapper;
        }
        public async Task<IEnumerable<PictureBooksDto>> GetAll()
        {
            var data = await _repo.PictureBooksRepository.Get();
            var attribute = _mapper.Map<IEnumerable<PictureBooks>, IEnumerable<PictureBooksDto>>(data);
            return attribute;
        }
        public async Task<IEnumerable<PictureBooksAdminDto>> GetAllByAdmin()
        {
            var data = await _repo.PictureBooksRepository.Get();
            var attribute = _mapper.Map<IEnumerable<PictureBooks>, IEnumerable<PictureBooksAdminDto>>(data);
            return attribute;
        }
        public async Task<PictureBooksDto> GetByID(int id)
        {
            var data = await _repo.PictureBooksRepository.Get(id);
            var attribute = _mapper.Map<PictureBooks, PictureBooksDto>(data);
            return attribute;
        }

        public async Task<PictureBooksDto> Insert(PictureBooksDto data)
        {
            var attribute = _mapper.Map<PictureBooksDto, PictureBooks>(data);
            var result = _context.PictureBooks.Add(attribute);
            var res = await _context.SaveChangesAsync();
            data.Id = result.Entity.Id;
            return data;
        }

        public async Task<bool> Update(int id, PictureBooksDto data)
        {
            var attribute = _mapper.Map<PictureBooksDto, PictureBooks>(data);
            bool result = _repo.PictureBooksRepository.Update(id, attribute);
            await _repo.SaveAsync();
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            bool result = _repo.PictureBooksRepository.Delete(id);
            await _repo.SaveAsync();
            return result;
        }
    }
}
