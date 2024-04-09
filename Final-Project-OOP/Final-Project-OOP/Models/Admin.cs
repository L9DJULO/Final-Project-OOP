using Google.Cloud.Firestore;

namespace Final_Project_OOP.Models
{
	public class Admin : User

	{
		[FirestoreProperty] public string AdminId { get; set; }
		[FirestoreProperty] public string FirstName { get; set; }
		[FirestoreProperty] public string LastName { get; set; }
	}
}
