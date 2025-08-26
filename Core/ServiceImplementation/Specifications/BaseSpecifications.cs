using Domain_Layer.Models;
using Domain_Layer.Repository_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service_Implementation.Specifications
{
    abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected BaseSpecifications(Expression<Func<TEntity, bool>>? CriteriaExpression)
        {
            WhereExpression = CriteriaExpression;
        }
        public Expression<Func<TEntity, bool>>? WhereExpression { get; private set; }

        #region Sorting

        public Expression<Func<TEntity, object>> OrderByExpression { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDescExpression { get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExp) => OrderByExpression = orderByExp;

        protected void AddOrderByDesc(Expression<Func<TEntity, object>> OrderByDescExp) => OrderByDescExpression = OrderByDescExp;
       
        #endregion

        #region Include

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
          => IncludeExpressions.Add(includeExpression);

        #endregion

        #region Pagination

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginated { get ; set ; }

        protected void ApplyPagination(int pageSize, int pageIndex)
        {
            IsPaginated = true;
            Take = pageSize;
            Skip = (pageIndex - 1) * pageSize;
        }

        #endregion
    }
}
