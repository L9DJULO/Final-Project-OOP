using Google.Cloud.Firestore;
using Final_Project_OOP.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Final_Project_OOP.Controllers
{
    public class FacultyMemberController : Controller
    {
        private readonly FirestoreDb _firestoreDb;

        public FacultyMemberController(IConfiguration configuration)
        {
            // Initialise Firestore DB connection
            string projectId = configuration["Firebase:ProjectId"];
            _firestoreDb = FirestoreDb.Create(projectId);
        }

        
        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult CreateAssignment(string courseId)
        {
            ViewData["CourseId"] = courseId;
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateAssignment(string courseId, Assignment assignment)
        {
            try
            {
                DocumentReference courseRef = _firestoreDb.Collection("Courses").Document(courseId);
                CollectionReference assignmentsRef = courseRef.Collection("Assignments");
                await assignmentsRef.AddAsync(assignment);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(assignment);
            }
        }

        
        public async Task<IActionResult> AddGradeToStudentInCourse(string courseId, string studentId, string assignmentId, int grade)
        {
            try
            {
                DocumentReference studentRef = _firestoreDb.Collection("Courses").Document(courseId)
                    .Collection("Students").Document(studentId)
                    .Collection("Assignments").Document(assignmentId);

                await studentRef.SetAsync(new { Grade = grade }, SetOptions.MergeAll);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View("Error");
            }
        }

        
    }
}
