namespace Final_Project_OOP.Models
{
    public class FacultyMember : User
    {
        public int FacultyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Course> Courses { get; set; }

        public List<int> CoursesId { get; set; }

    }
}
