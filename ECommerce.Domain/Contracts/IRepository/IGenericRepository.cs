using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain.Contracts.Specification;
using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Contracts.IRepository
{
    #region GenericRepository
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TKey id);
        Task AddAsync(TEntity entity);
        void UpdateAsync(TEntity entity);
        void DeleteAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllWithSpecificationAsync(ISpecifications<TEntity, TKey> specifications);
        Task<TEntity> GetByIdWithSpecificationAsync(ISpecifications<TEntity, TKey> specifications);
        Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications);
    }
    #endregion
}
