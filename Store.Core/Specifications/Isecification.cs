using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Specifications
{
	public interface Isecification<T> where T : BaseClass
	{
		// sign of where condition 
        public Expression<Func<T,bool>> Criteria { get; set; }

        // sign for list of include 
        public List<Expression<Func<T,object>>> includs  { get; set; }

        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDece { get; set; }

       public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnable { get; set; }

		// _dbcontext.set<Product>.Where(X=>X.id==id).Include(P=>P.ProductPrand).include(P=>P.ProductType);


	}

}
