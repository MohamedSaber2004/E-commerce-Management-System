using Domain_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Repository_Interfaces
{
    public interface ISpecifications<TEntity,TKey> where TEntity:BaseEntity<TKey>
    {
        // Property Signature for each dynamic part in a query

        public Expression<Func<TEntity , bool>> WhereExpression { get;  }

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }

        public Expression<Func<TEntity, object>> OrderByExpression { get;  }

        public Expression<Func<TEntity, object>> OrderByDescExpression { get; }

        public int Take { get; }
        public int Skip { get; }

        public bool IsPaginated { get; set; }
    }
}
