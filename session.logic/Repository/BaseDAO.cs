using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using session.Logic.Services;

namespace session.Logic.Repository
{
	public class BaseDAO<T> : BaseService, IBaseDAO<T>
	{
		public BaseDAO(IConfiguration configuration): base(configuration)
		{

		}
		public async Task<int> Create(string storedProc, DynamicParameters parameters)
		{
			using (var conn = this.Connection)
			{
				var models = await DapperORM<T>.ExecuteScalar(Connection, storedProc, parameters);

				return models;
			}
		}

		public Task<bool> Delete(T model)
		{
			throw new NotImplementedException();
		}

		public async Task<T> Find(string storedProc, DynamicParameters parameters)
		{
			using (var conn = this.Connection)
			{
				var model = await DapperORM<T>.Execute(Connection, storedProc, parameters);

				return model;
			}
			
		}

		public async Task<IEnumerable<T>> FindAll(string storedProc, DynamicParameters parameters)
		{
			using (var conn = this.Connection)
			{
				var model = await DapperORM<T>.ExecuteReturnList(conn, storedProc, parameters);

				return model;
			}
		}

		public Task<IEnumerable<T>> FindByEmail(string storedProc, DynamicParameters parameters)
		{
			throw new NotImplementedException();
		}

		public Task<int> FindExecuteScalar(string storedProc, DynamicParameters parameters)
		{
			throw new NotImplementedException();
		}
	}
}
