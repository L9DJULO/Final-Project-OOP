using Final_Project_OOP.Models;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Final_Project_OOP.Controllers
{
    public class HomeController : Controller
    {
        private readonly FirebaseAuthConfig _config;
        private readonly FirebaseAuthClient _authClient;
        private readonly FirebaseClient _client;
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
            _client = new FirebaseClient(configuration["Firebase:DatabaseUrl"]);
        }

        public async Task<IActionResult> Index(string email, string password)
        {
            try
            {
                var userCredential = await _authClient.SignInWithEmailAndPasswordAsync("email", "password");
                // Handle successful login
                return RedirectToAction("Dashboard", "Home");
            }
            catch (Exception ex)
            {
                // Handle login failure
                return View();
            }   
        }

        public async Task<ActionResult> CreateStudentAsync(Student model)
        {
            ViewData["Message"] = "";

            try
            {
                var userCredential = await _authClient.CreateUserWithEmailAndPasswordAsync(model.Email, model.Password, model.FirstName + " " + model.LastName);
                // Handle successful SignUp 
                //await _database.Collection("Users").Document(_authClient.User.Uid).SetAsync(model);
                var dino = await _client.Child("Users").PostAsync(model);

                return RedirectToAction(actionName:"Welcome", new {model});
            }
            catch (Exception ex)
            {
                // Handle SignUp failure
                TempData["ErrorMessage"] = ex.Message;
                return View(model);
            }
            
        }

        public ActionResult Welcome(Student model)
        {
            return View(model);
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
    }
}
