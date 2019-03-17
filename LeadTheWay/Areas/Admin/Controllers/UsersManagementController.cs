using System;
using System.Linq;
using System.Threading.Tasks;
using LeadTheWay.Data;
using LeadTheWay.Models;
using LeadTheWay.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeadTheWay.Areas.Admin.Controllers
{
    [Authorize(Roles = StaticDetails.AdminUser)]
    [Area("Admin")]
    public class UsersManagementController : Controller
    {
        private readonly ApplicationDbContext db;

        public UsersManagementController(ApplicationDbContext db)
        {
            this.db = db;
        }


        public IActionResult Index()
        {
            return View(db.ApplicationUsers.ToList());
        }

        //GET Edit
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || id.Trim().Length == 0)
            {
                return NotFound();
            }

            var user = await db.ApplicationUsers.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        //POST Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, ApplicationUser applicationUser)
        {
            if (id != applicationUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = db.ApplicationUsers.Where(u => u.Id == id).FirstOrDefault();
                user.Name = applicationUser.Name;
                user.PhoneNumber = applicationUser.PhoneNumber;

                user.GraphString = applicationUser.GraphString;
                ///TODO: user.Graph = Utility.StringToGraph(user.GraphString);

                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(applicationUser);
        }

        //GET Delete
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || id.Trim().Length == 0)
            {
                return NotFound();
            }

            var user = await db.ApplicationUsers.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        //POST Edit
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(string id)
        {
            var user = db.ApplicationUsers.Where(u => u.Id == id).FirstOrDefault();
            user.LockoutEnd = DateTime.Now.AddYears(1000);

            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}