using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models;

public class EmployeeContext : DbContext
{

    protected override void OnConfiguring
       (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "AuthorDb");
        }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }
}

public class Department
{
    public int DepartmentId { get; set; }
    public string Name { get; set; }

    public ICollection<Employee> Employees { get; set; }
}

public class Employee
{
    public int EmployeeId { get; set; }
    public int FirstName { get; set; }
    public int LastName { get; set; }
    public int Position { get; set; }

    public Department Department { get; set; }
}

