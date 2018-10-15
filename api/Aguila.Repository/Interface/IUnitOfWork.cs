using Aguila.DAL;
using System;
using System.Threading.Tasks;

namespace Aguila.Repository.Interface
{
    /// <summary>
    /// This class Unit of work interface
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Answers> AnswersRepository { get; }
        IRepository<Attributes> AttributesRepository { get; }
        IRepository<ConfigurationsValues> ConfigurationsValuesRepository { get; }
        IRepository<Handicaps> HandicapsRepository { get; }
        IRepository<PictureBooks> PictureBooksRepository { get; }
        IRepository<Lrddatas> LRDRepository { get; }
        IRepository<LrdattributesLookups> LRDattributesLookupsRepository { get; }
        IRepository<CategoryAttributes> CategoryAttributesRepository { get; }
        Task SaveAsync();
    }
}
