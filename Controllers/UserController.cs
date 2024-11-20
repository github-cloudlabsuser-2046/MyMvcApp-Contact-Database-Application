using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using System.Linq;

namespace MyMvcApp.Controllers
{
    public class UserController : Controller
    {
        public static List<User> userlist = new List<User>();

        // GET: User
        public ActionResult Index(string searchString)
        {
            var users = from u in userlist
                        select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.Name.Contains(searchString) || s.Email.Contains(searchString));
            }

            return View(users.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                userlist.Add(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, User updatedUser)
        {
            if (ModelState.IsValid)
            {
                var user = userlist.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    return NotFound();
                }

                user.Name = updatedUser.Name;
                user.Email = updatedUser.Email;
                // Update other properties as needed

                return RedirectToAction("Index");
            }
            return View(updatedUser);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            userlist.Remove(user);
            return RedirectToAction("Index");
        }
    }
}