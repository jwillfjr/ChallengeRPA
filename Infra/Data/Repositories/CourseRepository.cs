using Dapper;
using DapperExtensions;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Slapper.AutoMapper;

namespace Data.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<Course?> GetByTitleAsync(string title)
        {
            using var connection = new SqlConnection(base.connectionString);
            await connection.OpenAsync();
            var sql = "SELECT * FROM Course WHERE Title = @Title";
            var result = await connection.QueryFirstOrDefaultAsync<Course>(sql, new { Title = title });
            return result;
        }
    }
}
