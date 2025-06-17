using Store.Core.Entities;
using Store.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core
{
	public interface IUnitOfWork:IAsyncDisposable
	{
		IGenericRepository<IEntity> Repository<IEntity>() where IEntity : BaseClass;
		Task<int> ComplateAsync();
		
	}
}
