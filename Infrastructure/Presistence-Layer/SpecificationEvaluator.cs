using Domain_Layer.Models;
using Domain_Layer.Repository_Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence_Layer
{
    static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity,TKey>(IQueryable<TEntity> InputQuery,
                                                        ISpecifications<TEntity,TKey> specifications) where TEntity:BaseEntity<TKey>
        {
            var query = InputQuery;

            if(specifications.WhereExpression is not null)
            {
                query = query.Where(specifications.WhereExpression);
            }
            if(specifications.OrderByExpression  is not null)
            {
                query = query.OrderBy(specifications.OrderByExpression);
            }
            if(specifications.OrderByDescExpression is not null)
            {
                query = query.OrderByDescending(specifications.OrderByDescExpression);
            }
            if(specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Count > 0)
            {
                query = specifications.IncludeExpressions.Aggregate(query,(currentQuery, IncludeExpression) => currentQuery.Include(IncludeExpression));
            }
            if (specifications.IsPaginated)
            {
                query = query.Skip(specifications.Skip).Take(specifications.Take);
            }

            return query;
        }
    }
}
