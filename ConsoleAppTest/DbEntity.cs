using System.Data.Entity;

namespace ConsoleAppTest
{
    public class DbEntity : DbContext
    {
        public DbEntity()
            : base("name=DbEntity")
        {
        }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Departament> Departaments { get; set; }

    }
}