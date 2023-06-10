using Data.Models;
using OnlineCatalog.Dtos.MarkDtos;

namespace OnlineCatalog.Utils
{
    public static class MarkUtils
    {
        public static MarkDto? ToDto(this Mark mark)
          => mark is null ? null
                  : new MarkDto
                  {
                      Id = mark.Id,
                      Value = mark.Value,
                      CreatAt = mark.CreatAt,
                      CoursId = mark.CoursId,
                      CoursName= mark.Cours.Name
                  };

        public static IEnumerable<MarkDto> ToDto(this IEnumerable<Mark> marks)
           => marks is null ? null
               : marks.Select(s => s.ToDto());

        public static Mark ToEntity(this MarkCreateDto mark)
        {
            if (mark is null)
            {
                return null;
            }

            return new Mark
            {
                Value = mark.Value,                
                CoursId = mark.CoursId,               
                StudentId = mark.StudentId                
            };
        }
    }
}
