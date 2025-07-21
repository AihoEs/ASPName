using Microsoft.EntityFrameworkCore;
namespace WebApplication5.Pages.User
{
    public class DbUser : DbContext
    {
        public DbUser(DbContextOptions<DbUser> options) : base (options) { }
        public DbSet<UserInfo> UserList { get; set; }
    }
}
