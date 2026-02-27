using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Contracts.Specification
{
    #region SpecificationDesignPattern
    public interface ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
        public Expression<Func<TEntity, bool>> Criteria { get; }
        public Expression<Func<TEntity, object>>? SortAsc { get; }
        public Expression<Func<TEntity, object>>? SortDesc { get; }
        public int take { get; }
        public int skip { get; }
        public bool IsPaginated { get; }
    }
    #endregion
}
