namespace Final_Project_OOP.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public List<Assignment> Assignments { get; set; }
        // Add more properties as needed
    }
}
