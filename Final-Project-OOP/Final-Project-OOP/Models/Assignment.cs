namespace Final_Project_OOP.Models
{
    public class Assignment
    {
        public int AssignmentId { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
