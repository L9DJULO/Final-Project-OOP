namespace Final_Project_OOP.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public FacultyMember Lecturer { get; set; }

        public int LecturerId { get; set; }
        public List<Student> Students { get; set; }

        public List<int> StudentsId { get; set;}

        public List<Assignment> Assignments { get; set; }
        public List<int> AssignmentsId { get; set;}


        // Add more properties as needed
    }
}
