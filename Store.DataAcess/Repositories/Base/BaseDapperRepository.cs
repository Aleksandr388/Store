using Store.DataAcess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Options;
using Store.DataAcess.Options;
using Store.DataAcess.Entities;

namespace Store.DataAcess.Repositories.Base
{
    public class BaseDapperRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {

        private readonly ConnectionStrings _connectionString;

        public BaseDapperRepository(IOptions<ConnectionStrings> options)
        {
            _connectionString = options.Value;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_connectionString.DefaultConnection))
            {
                await connection.OpenAsync();

                var invoices = await connection.GetAllAsync<TEntity>();

                return invoices;
            }
        }

        public virtual async Task<TEntity> GetByIdAsync(long id)
        {
            using (var connection = new SqlConnection(_connectionString.DefaultConnection))
            {
                connection.Open();

                var invoice = await connection.GetAsync<TEntity>(id);

                return invoice;
            }
        }

        public Task SaveChagesAsync()
        {
            throw new NotImplementedException();
        }

        public virtual async Task UpdateAsync(TEntity model)
        {
            using (var connection = new SqlConnection(_connectionString.DefaultConnection))
            {
                await connection.OpenAsync();

                await connection.UpdateAsync(model);
            }
        }

        public virtual async Task CreateAsync(TEntity model)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString.DefaultConnection))
            {
                await connection.OpenAsync();

                await connection.InsertAsync(model);
            }

        }
        public Task DeleteAsync(TEntity model)
        {
            throw new NotImplementedException();
        }
    }
}
