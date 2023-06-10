using Data.Exceptions;
using Data.Extensions;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data;

namespace Data
{

    public class DataAccesLayer
    {
        private readonly IStudentDbContext _ctx;

        public DataAccesLayer(IStudentDbContext ctx)
            => _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));

        public void Seed()
        {
#if (DEBUG)
            _ctx.EnsureReset();
#endif

            Console.WriteLine(_ctx.Students.GetService<ICurrentDbContext>().Context.ContextId.InstanceId);
            var studentsSeed = new Student[] {
                new Student { Name = "Popescu Dorin", Address = new StudentAddress { Street = "Bulervadul Dacia", Number = 1, City="Ploiesti" }, Age = 22, Courses = new List<Cours>(), Marks = new List<Mark>() },
                new Student { Name = "Aldo Marina", Address = new StudentAddress { Street = "Str. Marian Moldoveanu", Number = 9, City = "Bucuresti" }, Age = 28, Courses = new List<Cours>(), Marks = new List<Mark>() },
                new Student { Name = "Barbu Carina", Address = new StudentAddress { Street = "Str. Stanilesti", Number = 1, City = "Iasi" }, Age = 35, Courses = new List<Cours>(), Marks = new List<Mark>() },
                new Student { Name = "Vasile Mirela", Address = new StudentAddress { Street = "Bulervadul Expozitiei", Number = 7, City = "Ploiesti" }, Age = 38, Courses = new List<Cours>(), Marks = new List<Mark>() },
                new Student { Name = "Toma Constantin", Address = new StudentAddress { Street = "Str. Panciu", Number = 5, City = "Ploiesti" }, Age = 41, Courses = new List<Cours>(), Marks = new List<Mark>() },
                new Student { Name = "Miu Ioana", Address = new StudentAddress { Street = "Str. Persani", Number = 15, City = "Bucuresti" }, Age = 34, Courses = new List<Cours>(), Marks = new List<Mark>() },
            };

            _ctx.Students.AddRange(studentsSeed);

            var courses = new Cours[]
            {
                new Cours { Name = "English"},
                new Cours { Name = "Math"}
            };
            _ctx.Courses.AddRange(courses);

            var teacher = new Teacher[]
            {
                new Teacher{ Name = "Saulescu Popa", Address = new TeacherAddress{ City = "Ploiesti", Street="Str. Coralilor", Number=1}, Cours = courses[0] },
                new Teacher{ Name = "Mirela Safari", Address = new TeacherAddress{ City = "Bucuresti", Street="Str. Ion Luca Caragiale", Number=1}, Cours = courses[1]},
            };
            _ctx.Teachers.AddRange(teacher);

            var marks = new Mark[]
            {
                new Mark{ Cours = courses[0], Student = studentsSeed[0], Value=8, Teacher= teacher[0]},
                new Mark{ Cours = courses[0], Student = studentsSeed[0], Value=5, Teacher= teacher[0]},
                new Mark{ Cours = courses[1], Student = studentsSeed[0], Value=10, Teacher= teacher[1]},

                new Mark{ Cours = courses[0], Student = studentsSeed[1], Value=8, Teacher= teacher[0]},
                new Mark{ Cours = courses[0], Student = studentsSeed[1], Value=9, Teacher= teacher[0]},
                new Mark{ Cours = courses[1], Student = studentsSeed[1], Value=3, Teacher= teacher[1]},

                new Mark{ Cours = courses[0], Student = studentsSeed[2], Value=7, Teacher= teacher[0]},
                new Mark{ Cours = courses[0], Student = studentsSeed[2], Value=10, Teacher= teacher[0]},

                new Mark{ Cours = courses[0], Student = studentsSeed[3], Value=8, Teacher= teacher[0]},

                new Mark{ Cours = courses[1], Student = studentsSeed[4], Value=10, Teacher= teacher[1]},
            };

            _ctx.Marks.AddRange(marks);
            _ctx.SaveChanges();
        }


        public IEnumerable<Student> GetStudents()
            => _ctx.Students.ToList();

        public Student? GetStudentById(int id)
            => _ctx.Students.FirstOrDefault(s => s.Id == id);

        public Student? CreateStudent(Student student)
        {
            if(_ctx.Students.Any(s=> s.Id== student.Id)) {
                throw new DuplicateNameException($"Student with id: {student.Id} already exists");
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

        public void UpdateStudentAddress(int studentId, Address? newAddress)
        {
            newAddress = newAddress ?? throw new ArgumentNullException(nameof(newAddress));

            var student = _ctx.Students.Include(s => s.Address).FirstOrDefault(s => s.Id == studentId) 
                ?? throw new Exception("Student does not exist");
            student.Address ??= new StudentAddress();
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


        public IEnumerable<Cours> GetCourses()
           => _ctx.Courses.ToList();

        public Cours? GetCourseById(int id)
          => _ctx.Courses.FirstOrDefault(s => s.Id == id);

        public Cours? CreateCourse(Cours course)
        {
            if (_ctx.Courses.Any(s => s.Id == course.Id))
            {
                throw new DuplicateNameException($"Cours with id: {course.Id} already exists");
            }
            _ctx.Courses.Add(course);
            _ctx.SaveChanges();
            return course;
        }

        public void RemoveCourse(int idCours)
        {
            var course = _ctx.Courses.FirstOrDefault(c => c.Id == idCours);
            if (course == null)
            {
                throw new InvalidIdException($"Cours doesent exist");
            }
            _ctx.Courses.Remove(course);
            _ctx.SaveChanges();
        }

        public IEnumerable<Mark> GetMarks()
          => _ctx.Marks.Include(m => m.Cours).ToList();

        public IEnumerable<Mark>? GetMarksByStudentId(int studentId)
         => _ctx.Marks.Include(c => c.Cours).Where(s=> s.StudentId == studentId).ToList();
        public IEnumerable<Mark>? GetMarksByStudentIdAndCourseId(int studentId, int courseId)
         => _ctx.Marks.Include(c => c.Cours).Where(s => s.StudentId == studentId && s.CoursId == courseId).ToList();

        public Mark? AddMark(Mark mark)
        {
            if (_ctx.Marks.Any(x => x.Id == mark.Id))
            {
                throw new DuplicatedIdException($"Duplicated mark id {mark.Id}");
            }
            var student = _ctx.Students.FirstOrDefault(s => s.Id == mark.StudentId) ?? throw new InvalidIdException($"Invalid student id {mark.StudentId}");
            var cours = _ctx.Courses.Include(s => s.Students).Include(t=> t.Teacher).FirstOrDefault(c => c.Id == mark.CoursId) ?? throw new InvalidIdException($"Invalid cours id {mark.CoursId}");
            if (cours.Teacher is null)
            {
                throw new InvalidIdException($"Course needs to have a teacher");
            }

            if (!cours.Students.Any(s => s.Id == student.Id))
            {
                cours.Students.Add(student);
            }

            // As there is a 1:1 relation between teacher and course
            // but there is a chance to change the teacher for the specific course 
            // the current teacher of the course should be assigned to the added mark
            mark.Teacher = cours.Teacher;

            _ctx.Marks.Add(mark);
            _ctx.SaveChanges();
            return mark;
        }

        public Teacher? AddTeacher(Teacher teacher)
        {
            if (_ctx.Teachers.Any(s => s.Id == teacher.Id))
            {
                throw new DuplicateNameException($"Teacher with id: {teacher.Id} already exists");
            }
            _ctx.Teachers.Add(teacher);
            _ctx.SaveChanges();
            return teacher;
        }

        public void RemoveTeacher(int teacherId) 
        {
            var teacherToRemove = GetTeacherById(teacherId);
            _ctx.Teachers.Remove(teacherToRemove);
            _ctx.SaveChanges();
        }

        public void UpdateTeacherAddress(int teacherId, TeacherAddress? address)
        {
            address = address ?? throw new ArgumentNullException(nameof(address));
            var teacherToUpdate = GetTeacherById(teacherId);
            teacherToUpdate.Address = address;
            _ctx.SaveChanges();
        }

        public void AssignTeacherToCourse(int teacherId, int courseId)
        {
            var teacher = GetTeacherById(teacherId);
            var course = GetCourseById(courseId);
            teacher.Cours = course;
            _ctx.SaveChanges();
        }

        public void PromoteTeacher(int teacherId)
        {
            var teacherToPromote = GetTeacherById(teacherId);
            teacherToPromote.Rank = (TeacherRank) Math.Clamp(
                (int) teacherToPromote.Rank - 1, 
                (int) TeacherRank.Professor, 
                (int) TeacherRank.Instructor);

            _ctx.SaveChanges();
        }

        public Teacher GetTeacherById(int teacherId) 
            => _ctx.Teachers
                    .Include(t => t.Address)
                    .Include(t => t.Cours)
                    .FirstOrDefault(t => t.Id == teacherId)
                ?? throw new InvalidIdException($"The teacher[{teacherId}] does not exist");

        public IEnumerable<Mark> GetTeacherGivenMarks(int teacherId)
        {
            return _ctx.Marks.Include(m => m.Cours).Where(m => m.TeacherId == teacherId).ToList();
        }

        public double GetAverageStudentCoursMarks(int studentId, int coursId)
        {
            if (!_ctx.Students.Any(s => s.Id == studentId))
            {
                throw new InvalidIdException($"Invalid student id {studentId}");
            }

            return _ctx.Marks.Where(s => s.StudentId == studentId && s.CoursId== coursId).Average(a=> a.Value);
        }

        public List<T> GetStudentOrderByMarksAverage<T>(Func<Student,double,T> select, SortEnum sort)
        {

            var query = _ctx.Students
                .Where(s => s.Marks.Any())
                .Select(s => new {
                    Student = s,
                    AverageMark = s.Marks.GroupBy(mark => mark.CoursId)
                                    .Select(marks => marks.Average(mark => mark.Value))
                                    .Average()
                });
            query = sort == SortEnum.ASC
                ? query.OrderBy(sa => sa.AverageMark)
                : query.OrderByDescending(sa => sa.AverageMark); 
            
            return query.ToList().Select(s => select(s.Student,s.AverageMark)).ToList();           
        }
    }
}
