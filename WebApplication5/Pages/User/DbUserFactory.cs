using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using WebApplication5.Pages.User;

namespace WebApplication4.Pages.User
{
    public class DbUserFactory : IDesignTimeDbContextFactory<DbUser>
    {
        public DbUser CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DbUser>();
            var connectionString = config.GetConnectionString("DefaultConnection");

            optionsBuilder.UseNpgsql(connectionString); 

            return new DbUser(optionsBuilder.Options);
        }
    }
}