using Final_Project_OOP.Models;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Diagnostics;
using System.Reflection;

namespace Final_Project_OOP.Controllers
{
    public class HomeController : Controller
    {
        private readonly FirebaseAuthConfig _config;
        private readonly FirebaseAuthClient _authClient;
        private readonly FirestoreDb _db;
        private readonly FirestoreClient _client;
        public HomeController(IConfiguration configuration)
        {
            _config = new FirebaseAuthConfig
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
            _db  = FirestoreDb.Create(configuration["Firebase:ProjectId"]);
            //_client = FirestoreClientBuilder.
        }

        public async Task<IActionResult> Index(Models.User model)
        {
            return View(model);
        }

        public async Task<ActionResult> CreateStudentAsync(Student model)
        {
            ViewData["Message"] = "";

            try
            {
                var userCredential = await _authClient.CreateUserWithEmailAndPasswordAsync(model.email, model.Password, model.FirstName + " " + model.LastName);
                // Handle successful SignUp 
                //await _database.Collection("Users").Document(_authClient.User.Uid).SetAsync(model);
                /*TODO: Add User To Database*/
                CollectionReference collection = _db.Collection("users");
                DocumentReference document = await collection.AddAsync(new { model });
                return RedirectToAction(actionName:"Welcome", new {model});
            }
            catch (Exception ex)
            {
                // Handle SignUp failure
                TempData["ErrorMessage"] = ex.Message;
                return View(model);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Login(Models.User user, string role)
        {
            try
            {
                var userCredential = await _authClient.SignInWithEmailAndPasswordAsync(user.email, user.Password);
                // Handle successful login
                var userID = userCredential.User.Uid;

                // Redirect based on the selected role
                switch (role)
                {
                    case "Student":
                        return RedirectToAction("Index", "Student");
                    case "FacultyMember":
                        return RedirectToAction("Index", "FacultyMember");
                    case "Admin":
                        return RedirectToAction("Index", "Admin");
                    default:
                        // Handle invalid role or other cases
                        return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                // Handle login failure
                return RedirectToAction("Index", new { user });
            }
        }
    }
}
