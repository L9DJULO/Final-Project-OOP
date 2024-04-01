namespace Final_Project_OOP.Models
{
    public class Student : User
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public List<Course> Courses { get; set; }
        public List<int> CoursesId { get; set; }

        public Dictionary<Assignment, int> Grade {  get; set; }

        // Add more properties as needed
    }
}
