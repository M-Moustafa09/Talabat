using Store.Core.Entities;
using Store.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Repositories
{
	public interface IGenericRepository<T> where T : BaseClass
	{
		#region without-Spaci
		//Get All Product
		Task<IReadOnlyList<T>> GetAllAsync();
		//Get Product By Id
		Task<T> GetByIdAsync(int id);
		#endregion
		#region With-Spaci
		Task<IReadOnlyList<T>> GetAllAsyncWthSpace(Isecification<T> space);

		Task<T> GetByEntityAsyncWithSpace( Isecification<T> space);

		Task<int> GetCountOfProductsBySpec(Isecification<T> space);

		Task AddASync(T entity);
		void Delete(T entity);
		void Update(T entity);
		#endregion

	}
}
