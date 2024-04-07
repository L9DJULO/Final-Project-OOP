using Google.Cloud.Firestore;

namespace Final_Project_OOP.Models
{
    [FirestoreData]
    public class User
    {
        [FirestoreProperty] public string email { get; set; }
        [FirestoreProperty] public string Password { get; set; }
        [FirestoreProperty] public string Role { get; set; }
    }
}
