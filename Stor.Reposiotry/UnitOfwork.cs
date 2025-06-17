using Stor.Reposiotry.Data;
using Store.Core;
using Store.Core.Entities;
using Store.Core.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stor.Reposiotry
{
	public class UnitOfwork : IUnitOfWork
	{
		private readonly StoreContext _storeContext;
		private  Hashtable _repos;

		public UnitOfwork(StoreContext storeContext) 
		{
			_storeContext = storeContext;
			_repos = new Hashtable();
		}

		public async Task<int> ComplateAsync()
		{
			return await _storeContext.SaveChangesAsync();
		}

		public async ValueTask DisposeAsync()
		{
			await _storeContext.DisposeAsync();
		}

		public IGenericRepository<IEntity> Repository<IEntity>() where IEntity : BaseClass
		{
			var type=typeof(IEntity).Name;
			if (!_repos.ContainsKey(type))
			{
			var repository= new GenaricRepository<IEntity>(_storeContext);
				_repos.Add(type, repository);
			}
			return _repos[type] as IGenericRepository<IEntity>;
		}
	}
}
