using Data.Models;
using OnlineCatalog.Dtos.CourseDtos;

namespace OnlineCatalog.Utils
{
    public static class CoursUtils
    {
        public static CourseDto? ToDto(this Cours cours)
          => cours is null ? null
                  : new CourseDto
                  {
                      Id = cours.Id,
                      Name = cours.Name
                  };

        public static IEnumerable<CourseDto> ToDto(this IEnumerable<Cours> courses)
           => courses is null ? null
               : courses.Select(s => s.ToDto());

        public static Cours ToEntity(this CoursCreateDto cours)
        {
            if (cours is null)
            {
                return null;
            }

            return new Cours { Name = cours.Name };
        }
    }
}
