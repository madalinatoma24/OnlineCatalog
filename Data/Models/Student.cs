namespace Data.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public StudentAddress Address { get; set; } = new StudentAddress();
        public List<Cours> Courses { get; set; } = new List<Cours>();
        public List<Mark> Marks { get; set; } = new List<Mark>();
    }
}
