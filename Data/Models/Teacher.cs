
using System.Text.Json.Serialization;

namespace Data.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TeacherAddress Address { get; set; }
        public TeacherRank Rank { get; set; }
        public Cours? Cours { get; set; }
    }
 
    public enum TeacherRank : int
    {
        Professor=0,
        AssociateProfessor=1,
        AssistantProfessor=2,
        Instructor=3,
    }
}
