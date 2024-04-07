using Google.Cloud.Firestore;

namespace Final_Project_OOP.Models
{
    [FirestoreData]
    public class Student : User
    {
        [FirestoreProperty] public int StudentId { get; set; }
        [FirestoreProperty] public string FirstName { get; set; }
        [FirestoreProperty] public string LastName { get; set; }
        [FirestoreProperty] public string Address { get; set; }
        [FirestoreProperty] public List<Course> Courses { get; set; }
        [FirestoreProperty] public List<int> CoursesId { get; set; }
        [FirestoreProperty] public List<Assignment> Assignments { get; set; }
        [FirestoreProperty]  public Dictionary<Assignment, int> Grade {  get; set; }
    }
}
