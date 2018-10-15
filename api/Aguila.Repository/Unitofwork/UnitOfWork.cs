using Aguila.DAL;
using Aguila.Repository.Interface;
using Aguila.Repository.Repository;
using System;
using System.Threading.Tasks;

namespace Aguila.Repository.Unitofwork
{
    /// <summary>
    /// This class implement IUnitOfWork
    /// This class checks the object of the same class is created or not if not created the creates
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AguilaContext entities = null;
        private bool disposed = false;

        public UnitOfWork(AguilaContext dbContext)
        {
            entities = dbContext;
        }

        // Add all the repository handles here
        private IRepository<Answers> _answersRepository;
        private IRepository<Attributes> _attributesRepository;
        private IRepository<ConfigurationsValues> _configurationsValuesnswersRepository;
        private IRepository<Handicaps> _handicapsRepository;
        private IRepository<PictureBooks> _pictureBooksRepository;
        private IRepository<Lrddatas> _lrdRepository;
        private IRepository<LrdattributesLookups> _lrdattributesLookupsRepository;
        private IRepository<CategoryAttributes> _categoryAttributesRepository;


        // Add all the repository getters here
        public IRepository<Answers> AnswersRepository
        {
            get
            {
                if (_answersRepository == null)
                    _answersRepository = new Repository<Answers>(entities);
                return _answersRepository;
            }
        }
        public IRepository<Attributes> AttributesRepository
        {
            get
            {
                if (_attributesRepository == null)
                    _attributesRepository = new Repository<Attributes>(entities);
                return _attributesRepository;
            }
        }
        public IRepository<ConfigurationsValues> ConfigurationsValuesRepository
        {
            get
            {
                if (_configurationsValuesnswersRepository == null)
                    _configurationsValuesnswersRepository = new Repository<ConfigurationsValues>(entities);
                return _configurationsValuesnswersRepository;
            }
        }
        public IRepository<Handicaps> HandicapsRepository
        {
            get
            {
                if (_handicapsRepository == null)
                    _handicapsRepository = new Repository<Handicaps>(entities);
                return _handicapsRepository;
            }
        }
        public IRepository<PictureBooks> PictureBooksRepository
        {
            get
            {
                if (_pictureBooksRepository == null)
                    _pictureBooksRepository = new Repository<PictureBooks>(entities);
                return _pictureBooksRepository;
            }
        }
        public IRepository<Lrddatas> LRDRepository
        {
            get
            {
                if (_lrdRepository == null)
                    _lrdRepository = new Repository<Lrddatas>(entities);
                return _lrdRepository;
            }
        }

        public IRepository<LrdattributesLookups> LRDattributesLookupsRepository
        {
            get
            {
                if (_lrdRepository == null)
                    _lrdattributesLookupsRepository = new Repository<LrdattributesLookups>(entities);
                return _lrdattributesLookupsRepository;
            }
        }

        public IRepository<CategoryAttributes> CategoryAttributesRepository
        {
            get
            {
                if (_categoryAttributesRepository == null)
                    _categoryAttributesRepository = new Repository<CategoryAttributes>(entities);
                return _categoryAttributesRepository;
            }
        }
        public async Task SaveAsync()
        {
            await entities.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    entities.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
