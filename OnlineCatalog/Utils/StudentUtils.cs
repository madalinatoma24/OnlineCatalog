using Data.Models;
using OnlineCatalog.Dtos;
using OnlineCatalog.Dtos.StudentDtos;

namespace OnlineCatalog.Utils
{
    public static class StudentUtils
    {

        public static StudentDto? ToDto(this Student student)
            => student is null ? null 
                    : new StudentDto { 
                        Id = student.Id, 
                        Age = student.Age, 
                        Name = student.Name 
                    };
        public static IEnumerable<StudentDto> ToDto(this IEnumerable<Student> students)
            => students is null ? null
                : students.Select(s => s.ToDto());

        public static Student ToEntity(this StudentCreateDto student)
        {
            if(student is null)
            {
                return null;
            }

            return new Student { Name = student.Name, Age = student.Age };
        }

        public static Student ToEntity(this StudentUpdateDto student)
        {
            if (student is null)
            {
                return null;
            }

            return new Student {Id= student.Id, Name = student.Name, Age = student.Age };
        }

        public static T? ToEntity<T>(this AddressToupdateDto addressToUpdate)
            where T : Address, new()
            => addressToUpdate is null ? null 
                : new T { 
                    City=addressToUpdate.City, 
                    Number= addressToUpdate.Number, 
                    Street=addressToUpdate.Street
                };
        public static StudentAddress? ToStudentEntity(this AddressToupdateDto addressToUpdate)
            => addressToUpdate.ToEntity<StudentAddress>();
        public static TeacherAddress? ToTeacherEntity(this AddressToupdateDto addressToUpdate)
            => addressToUpdate.ToEntity<TeacherAddress>();


        public static StudentMarksAverageDto ToAverageDto(this Student student, double average)
        {
            return new StudentMarksAverageDto
            {
                Name = student.Name,
                Average = average,
            };
        }
    }
}
