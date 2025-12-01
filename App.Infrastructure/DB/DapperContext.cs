using App.Application.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.DB
{
    public class DapperContext : IDapperContext
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public IDbConnection CreateConnection()
            => new NpgsqlConnection(_connectionString);
        public async Task<int> ExecuteAsync(string query, object? parameters = null)
        {
            using var connection = CreateConnection();
            return await connection.ExecuteAsync(query, parameters);
        }

        public async Task<T> ExecuteScalarAsync<T>(string query, object? parameters = null)
        {
            using var connection = CreateConnection();
            return await connection.ExecuteScalarAsync<T>(query, parameters);
        }

        // Query single row
        public async Task<T> QuerySingleAsync<T>(string query, object? parameters = null)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<T>(query, parameters);
        }

        // Query multiple rows
        public async Task<IEnumerable<T>> QueryListAsync<T>(string query, object? parameters = null)
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<T>(query, parameters);
        }
    }
}
