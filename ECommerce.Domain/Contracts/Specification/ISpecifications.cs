using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
    internal static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> EntryPoint,
            ISpecifications<TEntity, TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var query = EntryPoint;
            if (specifications is not null)
            {
                if (specifications.Criteria is not null)
                {
                    query = query.Where(specifications.Criteria);
                }

                if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Any())
                {
                    query = specifications.IncludeExpressions
                        .Aggregate(query, (currentquery, includeExp) => currentquery.Include(includeExp));
                }

                if (specifications.SortAsc is not null)
                {
                    query = query.OrderBy(specifications.SortAsc);
                }

                if (specifications.SortDesc is not null)
                {
                    query = query.OrderByDescending(specifications.SortDesc);
                }

                if (specifications.IsPaginated)
                {
                    query = query.Skip(specifications.skip).Take(specifications.take);
                }
            }
            return query;
        }
    }
    #endregion
}
