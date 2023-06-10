using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Mark
    {
        public int Id { get; set; }
        [Range(1,10, ErrorMessage = "{0} can only be beteween {1} and {10}")]
        public int Value { get; set; }
        public DateTime CreatAt { get; set; } = DateTime.Now;
        
        public int? CoursId { get; set; }
        public Cours Cours { get; set; }
        
        public int StudentId { get; set; }
        public Student Student { get; set; }
        
        public int? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
    }
}
