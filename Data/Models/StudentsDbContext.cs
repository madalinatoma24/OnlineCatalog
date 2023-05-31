
using Microsoft.EntityFrameworkCore;

namespace Data.Models
{
    internal class StudentsDbContext : DbContext, IStudentDbContext
    {
        public DbSet<Student> Students { get; private set; }
        public DbSet<Address> Addresses { get; private set; }

        public StudentsDbContext()
            => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""E:\curs 3 lab  3 tema\OnlineCatalog\Data\Database.mdf"";Integrated Security=True");
        }
    }
}
