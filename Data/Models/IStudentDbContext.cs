using Microsoft.EntityFrameworkCore;

namespace Data.Models
{
    public interface IStudentDbContext
    {
        DbSet<Student> Students { get; }
        DbSet<StudentAddress> StudentAddress { get; }
        DbSet<TeacherAddress> TeacherAddress { get; }
        DbSet<Cours> Courses { get; }
        DbSet<Mark> Marks { get; }
        DbSet<Teacher> Teachers { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        bool EnsureCreated();
        bool EnsureDeleted();
        bool EnsureReset();
    }
}
