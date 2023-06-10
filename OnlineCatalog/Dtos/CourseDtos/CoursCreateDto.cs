using System.ComponentModel.DataAnnotations;

namespace OnlineCatalog.Dtos.CourseDtos
{
    public class CoursCreateDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Cours name cannot be empty")]
        public string Name { get; set; }
    }
}
