using Google.Cloud.Firestore;

namespace Final_Project_OOP.Models
{
    [FirestoreData]
    public class FacultyMember : User
    {
        [FirestoreProperty]
        public int FacultyId { get; set; }

        [FirestoreProperty]
        public string FirstName { get; set; }

        [FirestoreProperty]
        public string LastName { get; set; }


        [FirestoreProperty]
        public List<Course> Courses { get; set; }

        [FirestoreProperty]
        public List<int> CoursesId { get; set; }

    }
}
