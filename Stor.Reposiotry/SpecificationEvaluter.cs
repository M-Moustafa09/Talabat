using Microsoft.EntityFrameworkCore;
using Store.Core.Entities;
using Store.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stor.Reposiotry
{
	public static  class SpecificationEvaluter<T> where T : BaseClass
	{
		// _dbcontext.set<Product>.Where(X=>X.id==id).Include(P=>P.ProductPrand).include(P=>P.ProductType); 
		public static IQueryable<T> GEtQuery( IQueryable<T> Inputquery ,Isecification<T> space)
		{
			var query = Inputquery;//_dbcontext.set<Product>
			if (space.Criteria is not null)
			{
				query=query.Where(space.Criteria);//_dbcontext.set<Product>.Where(X=>X.id==id)
			}
			if(space.OrderBy is not null)
			{
				query=query.OrderBy(space.OrderBy);
			}
			if(space.OrderByDece is not null)
			{
				query=query.OrderByDescending(space.OrderByDece);
			}
			if(space.IsPaginationEnable)
			{
				query=query.Skip(space.Skip).Take(space.Take);
			}

			query= space.includs.Aggregate(query,(CurrentQuery,nextIncludeExpression)=> CurrentQuery.Include(nextIncludeExpression));
				return query;
		}

	}
}
