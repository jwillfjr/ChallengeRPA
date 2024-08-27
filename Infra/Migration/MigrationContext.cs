using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MigrationFactory
{
    public class MigrationContext: DbContext
    {

        public MigrationContext(DbContextOptions<MigrationContext> options)
        : base(options)
        {
        }
        public DbSet<Course> Course { get; set; }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MigrationContext>
    {
        public MigrationContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<MigrationContext>();
            var connectionString = configuration.GetConnectionString("RPA");
            builder.UseSqlServer(connectionString);
            return new MigrationContext(builder.Options);
        }
    }
}
