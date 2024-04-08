using Google.Cloud.Firestore;

namespace Final_Project_OOP.Models
{
    [FirestoreData]
    public class Assignment
    {
        [FirestoreProperty] public int AssignmentId { get; set; }
        [FirestoreProperty] public string Description { get; set; }
        [FirestoreProperty] public DateTime Deadline { get; set; }
        [FirestoreProperty]public DateTime ReleaseDate { get; set; }
        [FirestoreProperty]public int CourseId { get; set; }
        [FirestoreProperty]public Course Course { get; set; }
    }
}
