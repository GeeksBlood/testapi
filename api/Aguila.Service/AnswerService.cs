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
    /// This class have interface methods implementation of IAnswerService
    /// for Get, Get by ID, Create, Update and Delete Answers.
    /// </summary>
    public class AnswerService : IAnswerService
    {
        private readonly IUnitOfWork _repo;
        private readonly IMapper _mapper;
        public AnswerService(IUnitOfWork repo, IMapper mapper)
        {
            this._repo = repo;
            this._mapper = mapper;
        }
        public async Task<IEnumerable<AnswersDto>> GetAll()
        {
            IEnumerable<Answers> data = await _repo.AnswersRepository.Get();
            var answer = _mapper.Map<IEnumerable<Answers>, IEnumerable<AnswersDto>>(data);
            return answer;
        }
        public async Task<AnswersDto> GetByID(int id)
        {
            var data = await _repo.AnswersRepository.Get(id);
            var answer = _mapper.Map<Answers, AnswersDto>(data);
            return answer;
        }

        public async Task<bool> Insert(AnswersDto data)
        {
            var answer = _mapper.Map<AnswersDto, Answers>(data);
            bool result = _repo.AnswersRepository.Insert(answer);
            await _repo.SaveAsync();
            return result;
        }

        public async Task<bool> Update(int id, AnswersDto data)
        {
            var answer = _mapper.Map<AnswersDto, Answers>(data);
            bool result = _repo.AnswersRepository.Update(id, answer);
            await _repo.SaveAsync();
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            bool result = _repo.AnswersRepository.Delete(id);
            await _repo.SaveAsync();
            return result;
        }
    }
}
