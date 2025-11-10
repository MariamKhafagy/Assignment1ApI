using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceLayer
{
    public static class SpecificationEvaluator
    {
        //Creat Query
        //_decontext.Set<TEntity>,where(p=>p.Id==id && ).InClude(p=>p.ProductType)   

        public static IQueryable<TEntity> CreateQuery<TEntity, Tkey>(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, Tkey> specifications) 
                                                                                              where TEntity : BaseEntity<Tkey>
        { 
            var query = inputQuery;

            if (specifications.Criteria is not null)
              query=query.Where(specifications.Criteria);

            #region Ordering
            if (specifications.OrderBy is not null)
            {
                query = query.OrderBy(specifications.OrderBy);
            }

            if (specifications.OrderByDescending is not null)
            {
                query = query.OrderByDescending(specifications.OrderByDescending);

            }
            #endregion

            #region Incudes
            if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Count > 0)
                 query=specifications.IncludeExpressions.Aggregate(query,(current , IncludeExpression)=>current.Include(IncludeExpression));

            #endregion

            #region Pagination

            if (specifications.IsPaginated)
            { 
             query=query.Skip(specifications.Skip).Take(specifications.Take);
            }



            #endregion

            return query;
        }
    }
}
