using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace session.Logic.Repository
{
	public static class DapperORM<T>
	{
		public static async Task ExecuteWithoutReturn(IDbConnection connection, string Proc, DynamicParameters param)
		{
			using (var conn = connection)
			{
				conn.Open();

				await conn.QueryAsync<T>(Proc,
					param,
					commandType: CommandType.StoredProcedure);
			}
		}

		public static Task<IEnumerable<T>> ExecuteReturnList(IDbConnection connection, string Proc, DynamicParameters param)
		{
			using (var conn = connection)
			{
				conn.Open();

				return conn.QueryAsync<T>(Proc,
					param,
					commandType: CommandType.StoredProcedure);
			}
		}

		public static async Task<T> Execute(IDbConnection connection, string Proc, DynamicParameters param)
		{
			using (var conn = connection)
			{
				conn.Open();

				return await conn.QuerySingleAsync<T>(Proc,
					param,
					commandType: CommandType.StoredProcedure);
			}
		}
		public static async Task<int> ExecuteScalar(IDbConnection connection, string Proc, DynamicParameters param)
		{
			using (var conn = connection)
			{
				conn.Open();

				return await conn.ExecuteScalarAsync<int>(Proc,
					param,
					commandType: CommandType.StoredProcedure);
			}
		}
	}
}

