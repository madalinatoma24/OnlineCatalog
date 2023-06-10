
using Microsoft.EntityFrameworkCore;

namespace Data.Models
{
    public class StudentsDbContext : DbContext, IStudentDbContext
    {
        public DbSet<Student> Students { get; private set; }
        public DbSet<StudentAddress> StudentAddress { get; private set; }
        public DbSet<TeacherAddress> TeacherAddress { get; private set; }
        public DbSet<Mark> Marks { get; private set; }
        public DbSet<Cours> Courses { get; private set; }
        public DbSet<Teacher> Teachers { get; private set; }

        public StudentsDbContext(DbContextOptions<StudentsDbContext> options) : base(options)
        {
            EnsureCreated();
        }

        public bool EnsureCreated() => Database.EnsureCreated();
        public bool EnsureDeleted() => Database.EnsureDeleted();
        public bool EnsureReset() => EnsureDeleted() && EnsureCreated();
    }
}
