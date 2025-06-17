using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Specifications
{
	public class BaseSpecification<T> : Isecification<T> where T : BaseClass
	{
		public Expression<Func<T, bool>> Criteria { get ; set ; }
		public List<Expression<Func<T, object>>> includs { get; set; } = new List<Expression<Func<T, object>>> ();
		public Expression<Func<T, object>> OrderBy { get; set; }
		public Expression<Func<T, object>> OrderByDece { get; set; }
		public int Skip { get; set; }
		public int Take { get; set; }
		public bool IsPaginationEnable { get; set; }

		public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T, bool>>  _criteria)
        {
            Criteria = _criteria;
            
        }
        public void SetOrderBy(Expression<Func<T, object>> orderBy)
        {
            OrderBy = orderBy;
        }
		public void SetOrderByDece(Expression<Func<T, object>> orderBy)
		{
			OrderByDece = orderBy;
		}

		public void SetSKipTakeValue(int skip,int take)
		{
			IsPaginationEnable = true;
			Skip = skip;
			Take = take;
		}

	}
}
