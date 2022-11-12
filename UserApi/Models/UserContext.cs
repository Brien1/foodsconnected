using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models;

public class UserContext : DbContext
{

    protected override void OnConfiguring
       (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "AuthorDb");
        }
    public DbSet<User> Users { get; set; }
}

public class User
{
    public long userId { get; set; }
    public string username { get; set; }

}

