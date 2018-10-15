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
    /// This class have interface methods implementation of ILRDService
    /// for Get, Get by ID, Get by LRD type Create, Update and Delete LRD Values.
    /// </summary>
    public class LRDService : ILRDService
    {
        private readonly AguilaContext _entities;
        private readonly IUnitOfWork _repo;
        private readonly IMapper _mapper;
        public LRDService(AguilaContext entities, IUnitOfWork repo, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
            this._repo = repo;
        }

        public async Task<IEnumerable<LrddatasDto>> GetLRDData()
        {
            IQueryable<Lrddatas> data = _entities.Lrddatas
                .Include(x => x.Lrdcategories).Include(x => x.LrdattributesLookups);
            IEnumerable<LrddatasDto> lrd = _mapper.Map<IEnumerable<Lrddatas>, IEnumerable<LrddatasDto>>(await data.ToListAsync());
            return lrd;
        }
        public async Task<IEnumerable<LrddatasDto>> GetLessonsData()
        {
            IQueryable<Lrddatas> data = _entities.Lrddatas
                .Include(x => x.Lrdcategories).Include(x => x.LrdattributesLookups)
                .Where(x => x.Lrdtype == "L");
            IEnumerable<LrddatasDto> lessons = _mapper.Map<IEnumerable<Lrddatas>, IEnumerable<LrddatasDto>>(await data.ToListAsync());
            return lessons;
        }
        public async Task<IEnumerable<LrddatasDto>> GetRulesData()
        {
            IQueryable<Lrddatas> data = _entities.Lrddatas
                .Include(x => x.Lrdcategories).Include(x => x.LrdattributesLookups)
                .Where(x => x.Lrdtype == "R");
            IEnumerable<LrddatasDto> rules = _mapper.Map<IEnumerable<Lrddatas>, IEnumerable<LrddatasDto>>(await data.ToListAsync());
            return rules;
        }
        public async Task<IEnumerable<LrddatasDto>> GetDidData()
        {
            IQueryable<Lrddatas> data = _entities.Lrddatas
                .Include(x => x.Lrdcategories).Include(x => x.LrdattributesLookups)
                .Where(x => x.Lrdtype == "D");
            IEnumerable<LrddatasDto> didYouKnow = _mapper.Map<IEnumerable<Lrddatas>, IEnumerable<LrddatasDto>>(await data.ToListAsync());
            return didYouKnow;
        }

        public async Task<IEnumerable<LrdcategoriesDto>> GetLRDCategory()
        {
            IQueryable<Lrdcategories> data = _entities.Lrdcategories;
            IEnumerable<LrdcategoriesDto> category = _mapper.Map<IEnumerable<Lrdcategories>, IEnumerable<LrdcategoriesDto>>(await data.ToListAsync());
            return category;
        }

        public async Task<IEnumerable<LrdattributesDto>> GetLRDAttribute()
        {
            IQueryable<Lrdattributes> data = _entities.Lrdattributes.Include(x => x.LrdattributesLookups);
            IEnumerable<LrdattributesDto> attribute = _mapper.Map<IEnumerable<Lrdattributes>, IEnumerable<LrdattributesDto>>(await data.ToListAsync());
            return attribute;
        }

        public async Task<IEnumerable<LrdattributesDto>> GetLRDLessonsAttribute()
        {
            IQueryable<Lrdattributes> data = _entities.Lrdattributes.Include(x => x.LrdattributesLookups).Where(x => x.LrdattributesId == 1);
            IEnumerable<LrdattributesDto> attribute = _mapper.Map<IEnumerable<Lrdattributes>, IEnumerable<LrdattributesDto>>(await data.ToListAsync());
            return attribute;
        }

        public async Task<IEnumerable<LrdattributesDto>> GetLRDRulesAttribute()
        {
            IQueryable<Lrdattributes> data = _entities.Lrdattributes.Include(x => x.LrdattributesLookups).Where(x => x.LrdattributesId == 2);
            IEnumerable<LrdattributesDto> attribute = _mapper.Map<IEnumerable<Lrdattributes>, IEnumerable<LrdattributesDto>>(await data.ToListAsync());
            return attribute;
        }

        public async Task<LrddatasDto> Get(int id)
        {
            Lrddatas data = await _entities.Lrddatas
                .Include(x => x.Lrdcategories).Include(x => x.LrdattributesLookups)
                .Where(x => x.LrddataId == id).FirstOrDefaultAsync();

            LrddatasDto result = _mapper.Map<Lrddatas, LrddatasDto>(data);
            return result;
        }

        public async Task<bool> UpdateLRD(int id, LrddatasDto data)
        {
            Lrddatas lrdData = _mapper.Map<LrddatasDto, Lrddatas>(data);
            bool result = _repo.LRDRepository.Update(id, lrdData);
            await _repo.SaveAsync();
            return result;
        }

        public async Task<LrddatasDto> CreateLRD(LrddatasDto data)
        {
            if (data.LrddataId != 0)
            {
                Lrddatas lrdData = _mapper.Map<LrddatasDto, Lrddatas>(data);
                bool result2 = _repo.LRDRepository.Update(data.LrddataId, lrdData);
                await _repo.SaveAsync();
                return data;
            }
            else
            {
                Lrddatas lrdDatas = new Lrddatas();
                lrdDatas.Title = data.Title;
                lrdDatas.VideoUrl = data.VideoUrl;
                lrdDatas.ContentLocation = data.ContentLocation;
                lrdDatas.Text = data.Text;
                lrdDatas.Lrdtype = data.Lrdtype;
                var lrddataID = _entities.Lrddatas.Add(lrdDatas);
                await _entities.SaveChangesAsync();

                if (data.LrdattributesLookups != null)
                {
                    var lrdAttributeLookups = data.LrdattributesLookups.ToList();
                    for (int i = 0; i < lrdAttributeLookups.Count; i++)
                    {
                        LrdattributesLookups lrdAttribute = new LrdattributesLookups();
                        lrdAttribute.LrdattributesId = lrdAttributeLookups[i].LrdattributesId;
                        lrdAttribute.LrdcategoryId = lrdAttributeLookups[i].LrdcategoryId;
                        lrdAttribute.LrddataLrddataId = lrddataID.Entity.LrddataId;
                        _entities.LrdattributesLookups.Add(lrdAttribute);
                    }

                    var lrdId = await _entities.SaveChangesAsync();
                }
                LrddatasDto result = _mapper.Map<Lrddatas, LrddatasDto>(lrdDatas);
                return result;
            }
        }

        public async Task<LrddatasDto> DeleteLRD(int id)
        {
            LrddatasDto res = new LrddatasDto();
            bool result = false;
            var lrd = await _entities.LrdattributesLookups.Where(x => x.LrddataLrddataId == id).FirstOrDefaultAsync();
            if (lrd != null)
            {
                result = _repo.LRDattributesLookupsRepository.Delete(lrd.Id);
                await _repo.SaveAsync();
            }
            Lrddatas res2 = await _repo.LRDRepository.Get(id);
            result = _repo.LRDRepository.Delete(id);
            await _repo.SaveAsync();
            res = _mapper.Map<Lrddatas, LrddatasDto>(res2);

            return res;

        }
    }
}
