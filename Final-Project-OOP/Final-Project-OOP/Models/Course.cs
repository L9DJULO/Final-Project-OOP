using Google.Cloud.Firestore;

namespace Final_Project_OOP.Models
{
    [FirestoreData]
    public class Course
    {

        [FirestoreProperty]
        public int CourseId { get; set; }

        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public string Code { get; set; }

        [FirestoreProperty]
        public string Description { get; set; }

        [FirestoreProperty]
        public FacultyMember Lecturer { get; set; }

        [FirestoreProperty]
        public int LecturerId { get; set; }

        [FirestoreProperty] 
        public List<Student> Students { get; set; }

        [FirestoreProperty]
        public List<int> StudentsId { get; set; }

        [FirestoreProperty]
        public List<Assignment> Assignments { get; set; }

        [FirestoreProperty] public List<int> AssignmentsId { get; set; }
    }
}
