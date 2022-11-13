using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Models;


public interface IUserContext
{
    DbSet<User> User { get; }
    int SaveChanges();
 
}
public class UserContext : DbContext, IUserContext
{
       protected override void OnConfiguring
       (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "AuthorDb");
        }

    public DbSet<User> User { get; set; }
   

}