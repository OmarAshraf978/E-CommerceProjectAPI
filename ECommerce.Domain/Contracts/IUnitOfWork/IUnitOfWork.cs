using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain.Contracts.IRepository;
using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Contracts.IUnitOfWork
{
    #region UnitOfWork
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = [];

        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var EntityType = typeof(TEntity);
            if (_repositories.TryGetValue(EntityType, out object? repository))
            {
                return (IGenericRepository<TEntity, TKey>)repository;
            }
            var newRepo = new GenericRepository<TEntity, TKey>(_dbContext);
            _repositories[EntityType] = newRepo;
            return newRepo;
        }

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
    #endregion
}
