using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeadTheWay.Data;
using LeadTheWay.Utility;
using Microsoft.AspNetCore.Authorization;
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


        public IActionResult Index()
        {
            if (db.GraphMaps.Any())
            {
                return View(db.GraphMaps.ToList());
            }

            return View(null);
        }

        //GET Edit Map
        public IActionResult Edit()
        {
            return View();
        }

        //GET Create Map
        public IActionResult Create()
        {
            return View();
        }

        //POST Create Map
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMap()
        {
            return View();
        }
    }
}