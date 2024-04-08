using Final_Project_OOP.Models;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Final_Project_OOP.Controllers
{
    public class StudentController : Controller
    {
        private readonly FirestoreDb _db;
        private readonly FirebaseAuthClient _authClient;

        public StudentController(IConfiguration configuration) {

            FirebaseAuthConfig _config = new FirebaseAuthConfig
            {

                ApiKey = configuration["Firebase:ApiKey"],
                AuthDomain = configuration["Firebase:AuthDomain"],
                Providers = new FirebaseAuthProvider[]
                {
                    // Add and configure individual providers
                    //new GoogleProvider().AddScopes("email"),
                    new EmailProvider()
                    // ...
                },
                // WPF:
                UserRepository = new FileUserRepository("FirebaseSample"), // persist data into %AppData%\FirebaseSample
                // UWP:
                //UserRepository = new StorageRepository() // persist data into ApplicationDataContainer
            };

            _authClient = new FirebaseAuthClient(_config);
            _db = FirestoreDb.Create(configuration["Firebase:ProjectId"]);

        }

        public async Task<Student> GetStudentFromId(string StudentId)
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

            if (student == null)
            {
                throw new Exception("Student not found");
            }

            return student;
        }

        // GET: StudentController Info Page
        public async Task<ActionResult> Index(string StudentId)
        {
            

            return View(student);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public async Task<ActionResult> CreateAsync(Student model)
        {
            TempData["Message"] = "";

            try
            {
                var userCredential = await _authClient.CreateUserWithEmailAndPasswordAsync(model.email, model.Password, model.FirstName + " " + model.LastName);
                // Handle successful SignUp 
                //await _database.Collection("Users").Document(_authClient.User.Uid).SetAsync(model);
                /*TODO: Add User To Database*/
                CollectionReference collection = _db.Collection("users");
                model.StudentId = userCredential.User.Uid;
                model.Role = "Student";
                DocumentReference document = await collection.AddAsync(model);
                return RedirectToAction(actionName: "Index", new { StudentId = model.StudentId });
            }
            catch (Exception ex)
            {
                // Handle SignUp failure
                TempData["ErrorMessage"] = ex.Message;
                return View(model);
            }
        }

        public async Task<ActionResult> SignUpToCours(string studentID)
        {

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
