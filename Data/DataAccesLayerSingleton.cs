using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data;
using System.Diagnostics.Contracts;

namespace Data
{
    public class DataAccesLayerSingleton
    {
        private readonly IStudentDbContext _ctx;
        
        #region Singleton
        private static DataAccesLayerSingleton? instance;

        private DataAccesLayerSingleton(IStudentDbContext ctx)
            => _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));

        public static DataAccesLayerSingleton Instance 
            => instance ??= new DataAccesLayerSingleton(new StudentsDbContext());

        /// <summary>
        /// Use this method when we want to mock or use a different implementation of IStudentDbbContext 
        /// </summary>
        public static DataAccesLayerSingleton GetInstance(IStudentDbContext ctx) 
            => instance ??= new DataAccesLayerSingleton(ctx);
        #endregion


        public void Seed()
        {
            var studentsSeed = new Student[] {
                new Student { Name = "Popescu Dorin", Address = new Address { Street = "Bulervadul Dacia", Number = 1, City="Ploiesti" }, Age = 22 },
                new Student { Name = "Aldo Marina", Address = new Address { Street = "Str. Marian Moldoveanu", Number = 9, City = "Bucuresti" }, Age = 28 },
                new Student { Name = "Barbu Carina", Address = new Address { Street = "Str. Stanilesti", Number = 1, City = "Iasi" }, Age = 35 },
                new Student { Name = "Vasile Mirela", Address = new Address { Street = "Bulervadul Expozitiei", Number = 7, City = "Ploiesti" }, Age = 38 },
                new Student { Name = "Toma Constantin", Address = new Address { Street = "Str. Panciu", Number = 5, City = "Ploiesti" }, Age = 41 },
                new Student { Name = "Miu Ioana", Address = new Address { Street = "Str. Persani", Number = 15, City = "Bucuresti" }, Age = 34 },
            };
            _ctx.Students.AddRange(studentsSeed);
            _ctx.SaveChanges();
        }


        public IEnumerable<Student> GetStudents()
            => _ctx.Students.ToList();

        public Student? GetStudentById(int id)
            => _ctx.Students.FirstOrDefault(s => s.Id == id);

        public Student? CreateStudent(Student student)
        {
            if(_ctx.Students.Any(s=> s.Id== student.Id)) {
                throw new DuplicateNameException($"Studentul cu Id-ul {student.Id} a fost deja creat");
            }
            _ctx.Students.Add(student);
            _ctx.SaveChanges();
            return student;
        }

        
        public Student? UpdateStudent(Student student)
        {
            var studentFromDb = _ctx.Students.FirstOrDefault(s => s.Id == student.Id) 
                ?? throw new Exception($"Student with id: {student.Id}, does not exist");
            studentFromDb.Age = student.Age;
            studentFromDb.Name = student.Name;

            _ctx.SaveChanges();
            return student;
        }

        public void UpdateStudentAddress(int studentId, Address newAddress)
        {
            var student = _ctx.Students.Include(s => s.Address).FirstOrDefault(s => s.Id == studentId) 
                ?? throw new Exception("Studentul nu exista");

            student.Address.Number = newAddress.Number;
            student.Address.Street = newAddress.Street;
            student.Address.City = newAddress.City;
            _ctx.SaveChanges();
        }

        public void RemoveStudent(int id)
        {
            Console.WriteLine(_ctx.Students.GetService<ICurrentDbContext>().Context.ContextId.InstanceId);
            var student = _ctx.Students.Include(s => s.Address).FirstOrDefault(s => s.Id == id) 
                ?? throw new Exception("Studentul nu exista");
            _ctx.Students.Remove(student);         
            _ctx.SaveChanges();
        }

    }
}
