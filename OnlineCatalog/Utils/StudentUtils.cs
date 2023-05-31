using Data.Models;
using OnlineCatalog.Dtos;

namespace OnlineCatalog.Utils
{
    public static class StudentUtils
    {

        public static StudentTogetDto TogetDto(this Student student)
        {
            if (student is null)
            {
                return null;

            }
            return new StudentTogetDto { Id = student.Id, Age = student.Age, Name = student.Name };
        }

        public static Student ToEntity(this StudentToCreateDto student)
        {
            if(student is null)
            {
                return null;
            }

            return new Student { Name = student.Name, Age = student.Age };
        }

        public static Student ToEntity(this StudentToUpdateDto student)
        {
            if (student is null)
            {
                return null;
            }

            return new Student {Id= student.Id, Name = student.Name, Age = student.Age };
        }

        public static Student ToEntity(this StudentToRemoveDto student)
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
