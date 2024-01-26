using Crud.Models.Domain;
using Microsoft.EntityFrameworkCore; 

namespace Crud.Data

{
    public class CrudDbContext : DbContext
    {
        public CrudDbContext() 
        { 

        }

        public CrudDbContext(DbContextOptions<CrudDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees  { get; set; }


    }
}
