using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain.Contracts.IRepository;
using ECommerce.Domain.Contracts.Specification;
using ECommerce.Domain.Entities;
using ECommerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence.GenericRepo
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _dbContext;

        public GenericRepository(StoreDbContext dbContext)
        {
           _dbContext = dbContext;
        }
        public async Task AddAsync(TEntity entity) => await _dbContext.Set<TEntity>().AddAsync(entity);

        public void DeleteAsync(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbContext.Set<TEntity>().ToListAsync();

        public async Task<TEntity> GetByIdAsync(TKey id) => await _dbContext.Set<TEntity>().FindAsync(id);

        public void UpdateAsync(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);

        public async Task<IEnumerable<TEntity>> GetAllWithSpecificationAsync(ISpecifications<TEntity, TKey> specifications)
        => await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).ToListAsync();

        public async Task<TEntity> GetByIdWithSpecificationAsync(ISpecifications<TEntity, TKey> specifications)
        => await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).FirstOrDefaultAsync();

        public async Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications)
        => await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).CountAsync();
    }
}
