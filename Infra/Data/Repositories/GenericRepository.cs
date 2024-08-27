using Dapper;
using DapperExtensions;
using Domain.IRepositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;


namespace Data.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly IConfiguration configuration;
        protected readonly string connectionString;


        public GenericRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration.GetConnectionString("RPA");
        }
        public virtual async Task<long> AddAsync(T entity)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var result = await connection.InsertAsync(entity);
            return result;
        }
        public virtual async Task<dynamic?> AddMultiAsync(T[] entity)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var result = await connection.InsertAsync(entity);
            return result;
        }

        public virtual Task<long> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            using var connection = new SqlConnection(connectionString);
           
            await connection.OpenAsync();      
            
            return await connection.GetListAsync<T>(); 
        }

        public virtual Task<T> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<long> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
