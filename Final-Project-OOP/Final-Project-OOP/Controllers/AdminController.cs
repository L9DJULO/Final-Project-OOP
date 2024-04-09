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
		// Action for displaying the admin dashboard
		private readonly FirestoreDb _db;
		private readonly FirebaseAuthClient _authClient;

		public AdminController(IConfiguration configuration)
		{

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

		public async Task<Admin> GetAdminFromId(string AdminId)
		{
			Query query = _db.Collection("users").WhereEqualTo("AdminId",AdminId);
			QuerySnapshot querySnapshot = await query.GetSnapshotAsync();



			Admin admin = null;
			foreach (DocumentSnapshot document in querySnapshot.Documents)
			{
				admin = document.ConvertTo<Admin>();
			}

			if (admin == null)
			{
				throw new Exception("Admin not found");
			}

			return admin;
		}
		public IActionResult Dashboard()
        {
            var courses = new List<Course>(); // Get courses from the database

            return View(courses);
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

		// GET: AdminController/Create


		public async Task<ActionResult> CreateAsync(Admin model)
		{
			TempData["Message"] = "";

			try
			{
				var userCredential = await _authClient.CreateUserWithEmailAndPasswordAsync(model.email, model.Password, model.FirstName + " " + model.LastName);
				// Handle successful SignUp 
				//await _database.Collection("Users").Document(_authClient.User.Uid).SetAsync(model);
				/*TODO: Add User To Database*/
				CollectionReference collection = _db.Collection("users");
				model.AdminId = userCredential.User.Uid;
				model.Role = "Admin";
				DocumentReference document = await collection.AddAsync(model);
				return RedirectToAction(actionName: "Index", new { AdminId = model.AdminId });
			}
			catch (Exception ex)
			{
				// Handle SignUp failure
				TempData["ErrorMessage"] = ex.Message;
				return View(model);
			}
		}
		// POST: AdminController/CreateCourse
		[HttpPost]
		public async Task<ActionResult> CreateCourse(Course model)
		{
			var course = new Course
			{
				Name = model.Name,
				Code = model.Code,
				Students = model.Students,
				Description = model.Description,
				Lecturer = model.Lecturer,
				LecturerId = model.LecturerId,
				StudentsId = model.StudentsId,
				Assignments = model.Assignments,
				AssignmentsId = model.AssignmentsId
			};

			await _db.Collection("courses").AddAsync(course);

			return RedirectToAction("Index");
		}

		// POST: AdminController/CreateStudent
		[HttpPost]
		public async Task<ActionResult> CreateStudent(Student model)
		{
			var student = new Student
			{
				FirstName = model.FirstName,
				Address = model.Address,
				LastName = model.LastName,
				CoursesId = model.CoursesId,
				Assignments = model.Assignments,
				AssignmentIds = model.AssignmentIds,
				Grades = model.Grades,
				email = model.email,
				Password = model.Password,
				Role = model.Role

			};

			await _db.Collection("Student").AddAsync(student);

			return RedirectToAction("Index");
		}

		// POST: AdminController/CreateFacultyMember
		[HttpPost]
		public async Task<ActionResult> CreateFacultyMember(FacultyMember model)
		{
			var Lecturer = new FacultyMember
			{
				
				FirstName = model.FirstName,
				LastName= model.LastName,
				Courses = model.Courses,
				CoursesId= model.CoursesId,
				email = model.email,
				Password = model.Password,
				Role = model.Role

			};

			await _db.Collection("Student").AddAsync(Lecturer);

			return RedirectToAction("Index");
		}


		// GET: AdminController/Edit/5
		public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
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

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
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
