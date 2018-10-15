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
    /// This class have interface methods implementation of IHandicapService
    /// for Get, Get by ID, Create, Update and Delete Handicap.
    /// </summary>
    public class HandicapService : IHandicapService
    {
        private readonly IUnitOfWork _repo;
        private readonly IMapper _mapper;

        public HandicapService(IUnitOfWork repo, IMapper mapper)
        {
            this._repo = repo;
            this._mapper = mapper;
        }
        public async Task<IEnumerable<HandicapsDto>> GetAll()
        {
            IEnumerable<Handicaps> data = await _repo.HandicapsRepository.Get();
            var handicaps = _mapper.Map<IEnumerable<Handicaps>, IEnumerable<HandicapsDto>>(data);
            return handicaps;
        }
        public async Task<HandicapsDto> GetByID(int id)
        {
            var data = await _repo.HandicapsRepository.Get(id);
            var handicaps = _mapper.Map<Handicaps, HandicapsDto>(data);
            return handicaps;
        }

        public async Task<bool> Insert(HandicapsDto data)
        {
            var handicaps = _mapper.Map<HandicapsDto, Handicaps>(data);
            bool result = _repo.HandicapsRepository.Insert(handicaps);
            await _repo.SaveAsync();
            return result;
        }

        public async Task<bool> Update(int id, HandicapsDto data)
        {
            var handicaps = _mapper.Map<HandicapsDto, Handicaps>(data);
            bool result = _repo.HandicapsRepository.Update(id, handicaps);
            await _repo.SaveAsync();
            return result;
        }
        public async Task<bool> Delete(int id)
        {
            bool result = _repo.HandicapsRepository.Delete(id);
            await _repo.SaveAsync();
            return result;
        }
    }
}

