using Data.Models;

namespace OnlineCatalog.Dtos.TeacherDtos
{
    public class TeacherDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Rank { get; set; }
        public string Cours { get; set; }
    }
}
