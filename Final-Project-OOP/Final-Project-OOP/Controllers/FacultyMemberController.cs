using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project_OOP.Controllers
{
    public class FacultyMemberController : Controller
    {
        // GET: FacultyMemberController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FacultyMemberController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FacultyMemberController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FacultyMemberController/Create
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

        // GET: FacultyMemberController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FacultyMemberController/Edit/5
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

        // GET: FacultyMemberController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FacultyMemberController/Delete/5
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
