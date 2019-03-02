using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeadTheWay.Data;
using LeadTheWay.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadTheWay.Areas.Admin.Controllers
{
    [Authorize(Roles = StaticDetails.AdminUser)]
    [Area("Admin")]
    public class MapsManagementController : Controller
    {
        private readonly ApplicationDbContext db;


        public MapsManagementController(ApplicationDbContext db)
        {
            this.db = db;
        }


        // GET: MapsManagement
        public IActionResult Index()
        {
            return View(db.GraphMaps.ToList());

            //if (db.GraphMaps.Any())
            //{
            //    return View(db.GraphMaps.ToList());
            //}

            //return View(null);
        }

        // GET: MapsManagement/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MapsManagement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MapsManagement/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: MapsManagement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MapsManagement/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: MapsManagement/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: MapsManagement/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}