using Data.Models;
using OnlineCatalog.Dtos.TeacherDtos;

namespace OnlineCatalog.Utils
{
    public static class TeacherUtils
    {
        public static TeacherDto? ToDto(this Teacher teacher)
         => teacher is null ? null
                 : new TeacherDto
                 {
                   Name= teacher.Name,
                   Address = teacher.Address?.Street + teacher.Address?.Number,
                   Cours= teacher.Cours?.Name ?? "",
                   Rank = teacher.Rank.ToString()  
                 };

        public static Teacher? ToEntity(this TeacherCreateDto teacher)
            => teacher is null ? null
            : new Teacher { Name = teacher.Name };
    }
}
