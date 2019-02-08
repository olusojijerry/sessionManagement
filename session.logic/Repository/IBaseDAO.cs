using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace session.Logic.Repository
{
	public interface IBaseDAO<T>
	{
		Task<int> Create(string storedProc, DynamicParameters parameters);
		Task<T> Find(string storedProc, DynamicParameters parameters);
		Task<IEnumerable<T>> FindAll(string storedProc, DynamicParameters parameters);
		Task<bool> Delete(T model);
		Task<int> FindExecuteScalar(string storedProc, DynamicParameters parameters);
	}
}
