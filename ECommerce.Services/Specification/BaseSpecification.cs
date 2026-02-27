using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain.Contracts.Specification;
using ECommerce.Domain.Entities;

namespace ECommerce.Services.Specification
{
    internal abstract class BaseSpecification<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity, bool>> Criteria { get; }
        public BaseSpecification(Expression<Func<TEntity, bool>> CriteriaExpression)
        {
            Criteria = CriteriaExpression;
        }

        public Expression<Func<TEntity, object>>? SortAsc { get; private set; }
        protected void AddSortAsc(Expression<Func<TEntity, object>> sortAscExp)
        {
            SortAsc = sortAscExp;
        }

        public Expression<Func<TEntity, object>>? SortDesc { get; private set; }
        protected void AddSortDesc(Expression<Func<TEntity, object>> sortDescExp)
        {
            SortDesc = sortDescExp;
        }

        public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];
        protected void AddInclude(Expression<Func<TEntity, object>> includeExp)
        {
            IncludeExpressions.Add(includeExp);
        }

        public int take { get; private set; }
        public int skip { get; private set; }
        public bool IsPaginated { get; private set; }
        protected void ApplyPagination(int PageIndex, int PadeSize)
        {
            IsPaginated = true;
            take = PadeSize;
            skip = (PageIndex - 1) * PadeSize;
        }
    }
}
