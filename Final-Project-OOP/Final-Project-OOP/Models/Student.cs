using Google.Cloud.Firestore;

namespace Final_Project_OOP.Models
{
    [FirestoreData]
    public class Student : User
    {
        [FirestoreProperty] public string StudentId { get; set; }
        [FirestoreProperty] public string FirstName { get; set; }
        [FirestoreProperty] public string LastName { get; set; }
        [FirestoreProperty] public string Address { get; set; }
        [FirestoreProperty] public List<string> CoursesId { get; set; }
        [FirestoreProperty] public List<Assignment> Assignments { get; set; }
        [FirestoreProperty] public List<string> AssignmentIds { get; set; }
        [FirestoreProperty]  public List<(string assignmentId, int grade)> Grades {  get; set; }
    }
}
