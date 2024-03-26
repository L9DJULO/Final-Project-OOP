namespace Final_Project_OOP.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // Store hashed passwords
        public string Role { get; set; } // Admin, Faculty, Student, etc.
                                         // Add more properties as needed
    }
}
