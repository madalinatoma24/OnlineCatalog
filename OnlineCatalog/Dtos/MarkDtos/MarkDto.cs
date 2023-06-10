using System.ComponentModel.DataAnnotations;

namespace OnlineCatalog.Dtos.MarkDtos
{
    public class MarkDto
    {
        public int Id { get; set; }
        [Range(1, 10, ErrorMessage = "{0} can only be beteween {1} and {10}")]
        public int Value { get; set; }
        public DateTime CreatAt { get; set; } = DateTime.Now;
        public int? CoursId { get; set; }
        public string CoursName { get; set; }
    }
}
