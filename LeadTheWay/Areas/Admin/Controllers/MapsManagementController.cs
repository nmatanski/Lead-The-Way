using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeadTheWay.Areas.Admin.Models.DTOs;
using LeadTheWay.Data;
using LeadTheWay.GraphLayer.Map.Domain.Models;
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
            //if (db.GraphMaps.Any())
            //{
            //    return View(db.GraphMaps.ToList());
            //}

            //return View(null);

            return View(db.GraphMaps.ToList());
        }

        // GET: MapsManagement/Create
        public IActionResult Create()
        {
            //the Index table
            //Add node list with all created nodes in the DB and add button which adds the node to the nodehistory and updates the graphstring with the node
            //Add edge list with all creataed edges in the DB and add button which adds the edge to the edgehistory and updates the graphstring with the edge
            

            return View();
        }

        // POST: MapsManagement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SimpleGraph graphDTO)
        {
            if (ModelState.IsValid)
            {
                var graph = new GraphMap()
                {
                    Name = graphDTO.Name,
                    IsDefault = graphDTO.IsDefault,
                    NodeHistory = new List<string>(),
                    EdgeHistory = new List<string>()
                };
                db.Add(graph);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(graphDTO);
        }

        //// GET: MapsManagement/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var graph = await db.GraphMaps.FindAsync(id);

        //    if (graph == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(graph);
        //}

        //// POST: MapsManagement/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: MapsManagement/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var graph = await db.GraphMaps.FindAsync(id);

            if (graph == null)
            {
                return NotFound();
            }

            return View(graph);
        }

        // GET: MapsManagement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var graph = await db.GraphMaps.FindAsync(id);

            if (graph == null)
            {
                return NotFound();
            }

            return View(graph);
        }

        // POST: MapsManagement/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var graph = await db.GraphMaps.FindAsync(id);
            db.GraphMaps.Remove(graph);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}