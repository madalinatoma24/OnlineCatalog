using Data.Models;
using OnlineCatalog.Dtos;

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
            => students is null ? new List<StudentDto>()
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

        public static Student ToEntity(this StudentRemoveDto student)
        {
            if (student is null)
            {
                return null;
            }

            return new Student { Id = student.Id, Name = student.Name, Age = student.Age };
        }

        public static Address ToEntity(this AddressToupdateDto addressToUpdate)
        {
            if (addressToUpdate is null)
            {
                return null;
            }

            return new Address { City=addressToUpdate.City, Number= addressToUpdate.Number, Street=addressToUpdate.Street};
        }
    }
}
