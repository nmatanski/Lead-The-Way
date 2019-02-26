using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeadTheWay.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeadTheWay.Areas.User.Controllers
{
    [Authorize(Roles = StaticDetails.AdminUser + ", " + StaticDetails.EndUser)]
    [Area("User")]
    public class MapController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}