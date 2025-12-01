using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Interfaces
{
    public interface IDapperContext
    {
        IDbConnection CreateConnection();
        Task<int> ExecuteAsync(string query, object? parameters = null);
        Task<T> ExecuteScalarAsync<T>(string query, object? parameters = null);
        Task<T> QuerySingleAsync<T>(string query, object? parameters = null);
        Task<IEnumerable<T>> QueryListAsync<T>(string query, object? parameters = null);
    }
}
