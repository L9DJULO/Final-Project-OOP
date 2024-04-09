using Final_Project_OOP.Models;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Final_Project_OOP.Controllers
{
    public class AdminController : Controller
    {
        private readonly FirestoreDb _db;

        public AdminController(IConfiguration configuration)
        {
            _db = FirestoreDb.Create(configuration["Firebase:ProjectId"]);
        }

        // Action method to display the admin dashboard
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateCourse(Course model)
        {
            await _db.Collection("courses").AddAsync(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> CreateStudent(Student model)
        {
            // Ajoutez la logique pour définir le rôle de l'utilisateur comme "student"
            model.Role = "Student";
            await _db.Collection("users").AddAsync(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> CreateFacultyMember(FacultyMember model)
        {
            // Ajoutez la logique pour définir le rôle de l'utilisateur comme "faculty member"
            model.Role = "Faculty Member";
            await _db.Collection("users").AddAsync(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> DeleteUser(string userId)
        {
            await _db.Collection("users").Document(userId).DeleteAsync();
            return RedirectToAction("Index");
        }
    }
}