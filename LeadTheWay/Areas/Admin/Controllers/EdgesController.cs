using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadTheWay.Areas.Admin.Controllers
{
    public class EdgesController : Controller
    {
        // GET: Edges
        public ActionResult Index()
        {
            return View();
        }

        // GET: Edges/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Edges/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Edges/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: Edges/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Edges/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: Edges/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Edges/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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