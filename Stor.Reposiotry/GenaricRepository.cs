using Microsoft.EntityFrameworkCore;
using Stor.Reposiotry.Data;
using Store.Core.Entities;
using Store.Core.Repositories;
using Store.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stor.Reposiotry
{
	public class GenaricRepository<T> : IGenericRepository<T> where T : BaseClass
	{
		private readonly StoreContext _dbContext;

		#region without space 
		public GenaricRepository(StoreContext DbContext) //Ask CLR to inject an object of StoreContext  Implicitly
		{
			_dbContext = DbContext;
		}
		public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			if (typeof(T) == typeof(Product))
			{
				return (IReadOnlyList<T>)await _dbContext.produts.Include(P => P.ProductBrand).Include(P => P.ProductType).ToListAsync();

			}
			return await _dbContext.Set<T>().ToListAsync();

		}



		public async Task<T> GetByIdAsync(int id)
		{
			//return await _dbContext.Set<T>().Where(X => X.Id == id).FirstOrDefaultAsync();
			return await _dbContext.Set<T>().FindAsync(id);
		} 
		#endregion
		

		public async Task<IReadOnlyList<T>> GetAllAsyncWthSpace(Isecification<T> space)
		{
			return await SpecificationEvaluter<T>.GEtQuery(_dbContext.Set<T>(), space).ToListAsync();
		}

		public async Task<T> GetByEntityAsyncWithSpace(Isecification<T> space)
		{
			return await SpecificationEvaluter<T>.GEtQuery(_dbContext.Set<T>(), space).FirstOrDefaultAsync();
		}

		private  IQueryable<T> ApplySpecifications(Isecification<T> space)
		{
			return SpecificationEvaluter<T>.GEtQuery(_dbContext.Set<T>(), space);
		}
		public async Task<int> GetCountOfProductsBySpec(Isecification<T> space)
		{
			return await ApplySpecifications(space).CountAsync();
		}

		public async Task AddASync(T entity)
		{
         await _dbContext.Set<T>().AddAsync(entity);
		}

		public void Delete(T entity)
		{
			_dbContext.Set<T>().Remove(entity); 
		}

		public void Update(T entity)
		{
			_dbContext.Set<T>().Update(entity);
		}
	}
}
