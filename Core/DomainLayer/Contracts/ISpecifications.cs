using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public  interface ISpecifications<TEnity,TKey> where TEnity: BaseEntity<TKey>
    {
        public Expression<Func<TEnity,bool>>? Criteria { get; }
        public List<Expression<Func<TEnity,object>>>IncludeExpressions  { get;  }
        public  Expression<Func<TEnity,object>> OrderBy{ get;  }
        public Expression<Func<TEnity, object>> OrderByDescending { get; }

        public int Take { get; }

        public int Skip { get; }

        public bool IsPaginated { get;  }
    }
}
