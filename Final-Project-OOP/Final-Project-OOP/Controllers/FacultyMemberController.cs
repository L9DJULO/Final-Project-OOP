using Final_Project_OOP.Models;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project_OOP.Controllers
{
    public class FacultyMemberController : Controller
    {
        private readonly FirestoreDb _db;

        public FacultyMemberController(IConfiguration configuration)
        {
            _db = FirestoreDb.Create(configuration["Firebase:ProjectId"]);
        }

        // GET: FacultyMemberController
        public async Task<ActionResult> Index(string facultyMemberId)
        {
            Query query = _db.Collection("facultyMembers").WhereEqualTo("FacultyMemberId", facultyMemberId);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

            FacultyMember facultyMember = null;
            foreach (DocumentSnapshot document in querySnapshot.Documents)
            {
                facultyMember = document.ConvertTo<FacultyMember>();
            }

            if (facultyMember == null)
            {
                return NotFound();
            }

            return View(facultyMember);
        }

        // POST: FacultyMemberController/CreateAssignment
        [HttpPost]
        public async Task<ActionResult> CreateAssignment(Assignment model)
        {
            var assignment = new Assignment
            {
                CourseId = model.CourseId,
                Course = model.Course,
                Name = model.Name,
                Description = model.Description,
                Deadline = model.Deadline
            };

            await _db.Collection("assignments").AddAsync(assignment);

            return RedirectToAction("Index");
        }

        // POST: FacultyMemberController/GradeAssignment
        [HttpPost]
        public async Task<ActionResult> GradeAssignment(string assignmentId, Dictionary<string, double> studentGrades)
        {
            foreach (var studentGrade in studentGrades)
            {
                DocumentReference studentRef = _db.Collection("students").Document(studentGrade.Key);
                DocumentSnapshot studentDoc = await studentRef.GetSnapshotAsync();
                if (studentDoc.Exists)
                {
                    Dictionary<string, object> update = new Dictionary<string, object>
                    {
                        { $"Grades.{assignmentId}", studentGrade.Value }
                    };
                    await studentRef.UpdateAsync(update);
                }
            }

            return RedirectToAction("Index");
        }
    }
}
