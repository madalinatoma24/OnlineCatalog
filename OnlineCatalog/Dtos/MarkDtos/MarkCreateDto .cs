using System.ComponentModel.DataAnnotations;

namespace OnlineCatalog.Dtos.MarkDtos
{
    public class MarkCreateDto
    {
        [Range(1, 10, ErrorMessage = "{0} can only be beteween {1} and {10}")]
        public int Value { get; set; }
        public int StudentId { get; set; }
        public int CoursId { get; set; }
    }
}
