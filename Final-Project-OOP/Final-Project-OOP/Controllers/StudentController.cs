using Final_Project_OOP.Models;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Final_Project_OOP.Controllers
{
    public class StudentController : Controller
    {
        private readonly FirestoreDb _db;
        public StudentController(IConfiguration configuration) {

            _db = FirestoreDb.Create(configuration["Firebase:ProjectId"]);

        }

        // GET: StudentController
        public async Task<ActionResult> Index(string StudentId)
        {
            Query query = _db.Collection("users").WhereEqualTo("StudentId", StudentId);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

            /*DocumentReference studentDoc = _db.Collection("users").Document(StudentId);
            DocumentSnapshot snapshot = await studentDoc.GetSnapshotAsync();*/

            Student student = null;
            foreach (DocumentSnapshot document in querySnapshot.Documents)
            {
                student = document.ConvertTo<Student>();
            }

            if(student == null)
            {
                throw new Exception("Student noy found");
            }

            return View(student);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
