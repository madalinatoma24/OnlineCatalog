namespace Data.Models
{
    public abstract class Address
    {
        public int Id { get; set; }
        public string Street { get; set; } = "";
        public int Number { get; set; }
        public string City { get; set; } = "";
       
    }

    public class StudentAddress: Address
    {
        public int StudnetId { get; set; }
    }

    public class TeacherAddress : Address
    {
        public int TeacherId { get; set; }
    }
}
