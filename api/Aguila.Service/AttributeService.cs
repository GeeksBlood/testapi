using Aguila.DAL;
using Aguila.Models;
using Aguila.Repository.Interface;
using Aguila.Service.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aguila.Service
{
    /// <summary>
    /// This class have interface methods implementation of IAttributeService
    /// for Get, Get by ID, Create, Update and Delete Attribute.
    /// </summary>
    public class AttributeService : IAttributeService
    {
        private readonly AguilaContext _context;
        private readonly IUnitOfWork _repo;
        private readonly IMapper _mapper;

        public AttributeService(IUnitOfWork repo, IMapper mapper, AguilaContext context)
        {
            this._repo = repo;
            this._mapper = mapper;
            this._context = context;
        }
        public async Task<IEnumerable<AttributesDto>> GetAll()
        {
            IEnumerable<Attributes> data = await _repo.AttributesRepository.Get();
            var attribute = _mapper.Map<IEnumerable<Attributes>, IEnumerable<AttributesDto>>(data);
            return attribute;
        }
        public async Task<AttributesDto> GetByID(int id)
        {
            var data = await _repo.AttributesRepository.Get(id);
            var attribute = _mapper.Map<Attributes, AttributesDto>(data);
            return attribute;
        }

        public async Task<IEnumerable<AttributeInformation>> GetAttributesByCategory(int Categoryid)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryAttributes, AttributeInformation>().ForMember(dto => dto.Id,
                           map => map.MapFrom(x => x.AttributesId)).ForMember(dto => dto.AttributeDescription,
                           map => map.MapFrom(x => x.Attributes.AttributeDescr));
            });
            var mapper = config.CreateMapper();
            IQueryable<CategoryAttributes> data = _context.CategoryAttributes.Include(x => x.Attributes)
                .Where(a => a.Category.Id == Categoryid);
            IEnumerable<CategoryAttributes> result = await data.ToListAsync();
            var attribute = mapper.Map<IEnumerable<CategoryAttributes>, IEnumerable<AttributeInformation>>(result);
            return attribute;
        }

        public async Task<bool> Insert(AttributesDto data)
        {
            var attribute = _mapper.Map<AttributesDto, Attributes>(data);
            bool result = _repo.AttributesRepository.Insert(attribute);
            await _repo.SaveAsync();
            return result;
        }

        public async Task<bool> Update(int id, AttributesDto data)
        {
            var attribute = _mapper.Map<AttributesDto, Attributes>(data);
            bool result = _repo.AttributesRepository.Update(id, attribute);
            await _repo.SaveAsync();
            return result;
        }
        public async Task<bool> Delete(int id)
        {
            bool result = _repo.AttributesRepository.Delete(id);
            await _repo.SaveAsync();
            return result;
        }
    }
}
