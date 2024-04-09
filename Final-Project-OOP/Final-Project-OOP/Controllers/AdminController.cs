using Final_Project_OOP.Models;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            await _db.Collection("students").AddAsync(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> CreateFacultyMember(FacultyMember model)
        {
            await _db.Collection("facultyMembers").AddAsync(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> DeleteStudent(string studentId)
        {
            var studentRef = _db.Collection("students").Document(studentId);
            await studentRef.DeleteAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> DeleteCourse(string courseId)
        {
            var courseRef = _db.Collection("courses").Document(courseId);
            await courseRef.DeleteAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> DeleteFacultyMember(string facultyMemberId)
        {
            var facultyMemberRef = _db.Collection("facultyMembers").Document(facultyMemberId);
            await facultyMemberRef.DeleteAsync();
            return RedirectToAction("Index");
        }
    }
}
