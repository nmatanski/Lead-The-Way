using System.Linq;
using System.Threading.Tasks;
using LeadTheWay.Data;
using LeadTheWay.GraphLayer.Vertex.Domain.Models;
using LeadTheWay.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeadTheWay.Areas.Admin.Controllers
{
    [Authorize(Roles = StaticDetails.AdminUser)]
    [Area("Admin")]
    public class NodesController : Controller
    {
        private readonly ApplicationDbContext db;

        public NodesController(ApplicationDbContext db)
        {
            this.db = db;
        }


        //GET: Nodes
        public IActionResult Index()
        {
            return View(db.TransportVertices.ToList());
        }

        // GET: Nodes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nodes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransportVertex node)
        {
            if (ModelState.IsValid)
            {
                db.Add(node);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(node);
        }

        // GET: Nodes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var node = await db.TransportVertices.FindAsync(id);

            if (node == null)
            {
                return NotFound();
            }

            return View(node);
        }

        // POST: Nodes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TransportVertex node)
        {
            if (id != node.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                db.Update(node);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(node);
        }

        // GET: Nodes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var node = await db.TransportVertices.FindAsync(id);

            if (node == null)
            {
                return NotFound();
            }

            return View(node);
        }

        // GET: Nodes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var node = await db.TransportVertices.FindAsync(id);

            if (node == null)
            {
                return NotFound();
            }

            return View(node);
        }

        // POST: Nodes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var node = await db.TransportVertices.FindAsync(id);
            db.TransportVertices.Remove(node);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}