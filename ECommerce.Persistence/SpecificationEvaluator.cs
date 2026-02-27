using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain.Contracts.Specification;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> EntryPoint, 
            ISpecifications<TEntity, TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var query = EntryPoint;
            if (specifications is not null)
            {
                if(specifications.Criteria is not null)
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

                if(specifications.IsPaginated)
                {
                    query = query.Skip(specifications.skip).Take(specifications.take);
                }
            }
            return query;
        }
    }
}
