using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeadTheWay.Data;
using LeadTheWay.GraphLayer.Link.Domain.Models;
using LeadTheWay.Models.ViewModels;
using LeadTheWay.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeadTheWay.Areas.Admin.Controllers
{
    [Authorize(Roles = StaticDetails.AdminUser)]
    [Area("Admin")]
    public class EdgesController : Controller
    {
        private readonly ApplicationDbContext db;

        [BindProperty]
        public EdgeViewModel EdgeVM { get; set; }


        public EdgesController(ApplicationDbContext db)
        {
            this.db = db;

            EdgeVM = new EdgeViewModel()
            {
                Edge = new IntercityLink(),
                Nodes = db.TransportVertices.ToList()
            };
            EdgeVM.Edge.NodesPair = new VerticesPair();
        }


        // GET: Edges
        public IActionResult Index()
        {
            return View(db.IntercityLinks.ToList());
        }

        // GET: Edges/Create
        public IActionResult Create()
        {
            return View(EdgeVM);
        }

        // POST: Edges/Create
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(EdgeVM);
            //}
            var edge = EdgeVM.Edge;
            var firstNode = await db.TransportVertices.Where(n => n.Id == edge.NodesPair.FirstNodeId).FirstOrDefaultAsync();
            var relatedNode = await db.TransportVertices.Where(n => n.Id == edge.NodesPair.RelatedNodeId).FirstOrDefaultAsync();
            string isDirected = EdgeVM.IsDirected ? "->" : "<->";

            edge.EdgeString = $"{firstNode.Name}{isDirected}{relatedNode.Name}";

            edge.DurationTicks = EdgeVM.DurationSpan.Ticks;

            db.IntercityLinks.Add(edge);
            await db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Edges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var edge = await db.IntercityLinks.FindAsync(id);

            if (edge == null)
            {
                return NotFound();
            }

            return View(edge);
        }

        // POST: Edges/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IntercityLink edge)
        {
            if (id != edge.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                db.Update(edge);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(edge);
        }

        // GET: Edges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var edge = await db.IntercityLinks.FindAsync(id);

            if (edge == null)
            {
                return NotFound();
            }

            return View(edge);
        }

        // GET: Edges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var edge = await db.IntercityLinks.FindAsync(id);

            if (edge == null)
            {
                return NotFound();
            }

            return View(edge);
        }

        // POST: Edges/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var edge = await db.IntercityLinks.FindAsync(id);
            db.IntercityLinks.Remove(edge);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}