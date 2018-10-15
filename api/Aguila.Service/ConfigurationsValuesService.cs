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
    /// This class have interface methods implementation of IConfigurationsValuesService
    /// for Get, Get by ID, Create, Update and Delete Configurations Values.
    /// </summary>
    public class ConfigurationsValuesService : IConfigurationsValuesService
    {
        private readonly IUnitOfWork _repo;
        public ConfigurationsValuesService(IUnitOfWork repo)
        {
            this._repo = repo;
        }
        public async Task<IEnumerable<ConfigurationsValuesDto>> GetAll()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ConfigurationsValues, ConfigurationsValuesDto>().ForMember(dto => dto.ReminderFrequency,
                           map => map.MapFrom(x => x.ValueKey)).ForMember(dto => dto.ReminderText,
                           map => map.MapFrom(x => x.Value));
            });
            var mapper = config.CreateMapper();
            IEnumerable<ConfigurationsValues> data = await _repo.ConfigurationsValuesRepository.Get();
            var configurationsValues = mapper.Map<IEnumerable<ConfigurationsValues>, IEnumerable<ConfigurationsValuesDto>>(data);
            return configurationsValues;

        }
        public async Task<ConfigurationsValuesDto> GetByID(int id)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ConfigurationsValues, ConfigurationsValuesDto>().ForMember(dto => dto.ReminderFrequency,
                           map => map.MapFrom(x => x.ValueKey)).ForMember(dto => dto.ReminderText,
                           map => map.MapFrom(x => x.Value));
            });
            var mapper = config.CreateMapper();
            ConfigurationsValues data = await _repo.ConfigurationsValuesRepository.Get(id);
            var configurationsValues = mapper.Map<ConfigurationsValues, ConfigurationsValuesDto>(data);
            return configurationsValues;

        }

        public async Task<bool> Insert(ConfigurationsValuesDto data)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ConfigurationsValuesDto, ConfigurationsValues>().ForMember(dto => dto.ValueKey,
                           map => map.MapFrom(x => x.ReminderFrequency)).ForMember(dto => dto.Value,
                           map => map.MapFrom(x => x.ReminderText));
            });
            var mapper = config.CreateMapper();
            var configurationsValues = mapper.Map<ConfigurationsValuesDto, ConfigurationsValues>(data);
            bool result = _repo.ConfigurationsValuesRepository.Insert(configurationsValues);
            await _repo.SaveAsync();
            return result;
        }

        public async Task<bool> Update(int id, ConfigurationsValuesDto data)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ConfigurationsValuesDto, ConfigurationsValues>().ForMember(dto => dto.ValueKey,
                           map => map.MapFrom(x => x.ReminderFrequency)).ForMember(dto => dto.Value,
                           map => map.MapFrom(x => x.ReminderText));
            });
            var mapper = config.CreateMapper();
            var configurationsValues = mapper.Map<ConfigurationsValuesDto, ConfigurationsValues>(data);
            bool result = _repo.ConfigurationsValuesRepository.Update(id, configurationsValues);
            await _repo.SaveAsync();
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            bool result = _repo.ConfigurationsValuesRepository.Delete(id);
            await _repo.SaveAsync();
            return result;
        }
    }
}
