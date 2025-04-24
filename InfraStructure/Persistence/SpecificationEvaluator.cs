using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
  public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, Tkey>(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, Tkey> spec) where TEntity : ModelBase<Tkey>
        {
            var query = inputQuery;

            if (spec.Criteria != null)
                query = query.Where(spec.Criteria);




            if(spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);

           if (spec.OrderByDesc is not null)
                query = query.OrderByDescending(spec.OrderByDesc);




            if (spec.IncludeExpressions.Count > 0)
                query = spec.IncludeExpressions.Aggregate(query, (current, exp) => current.Include(exp));


            if (spec.IsPaginated == true)
                query=query.Skip(spec.Skip).Take(spec.Take);



           


            return query;
        }

    }
}
