using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace foods_connected_brien.Controllers
{
   public class DepartmentController : Controller
{
    private EmployeeContext db = new EmployeeContext();

    [HttpGet("{id}")]
    public List<Department> Index()
    {
        return db.Departments.OrderBy(d => d.Name).ToList();
    }
    
    protected override void Dispose(bool disposing)
    {
        db.Dispose();
        base.Dispose(disposing);
    }
}
}
