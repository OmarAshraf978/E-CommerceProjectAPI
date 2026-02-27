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
    
    #endregion
}
