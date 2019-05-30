using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LeadTheWay.Data;
using LeadTheWay.GraphLayer.Map.Service;
using LeadTheWay.GraphLayer.Vertex.Service;
using LeadTheWay.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeadTheWay.Areas.User.Controllers
{
    //[Authorize(Roles = StaticDetails.AdminUser + ", " + StaticDetails.EndUser)]
    [Area("User")]
    public class MapController : Controller
    {
        private readonly ApplicationDbContext db;

        private readonly UserManager<IdentityUser> identityUserManager;

        public MapController(ApplicationDbContext db, UserManager<IdentityUser> identityUserManager)
        {
            this.db = db;
            this.identityUserManager = identityUserManager;

        }


        public async Task<IActionResult> Index()
        {
            var user = await GetApplicationUserWithDefaultGraphMapFromClaimsPrincipalUser(User);

            ///TODO:
            //var result = Graph.Search(new CheapestPathSearch(user.Map.Graph), "Plovdiv", "Varna");
            //var cheapest = GraphUtil.ResultCheapest;
            //cheapest += result + '\n';

            return View(user);
        }

        private async Task<ApplicationUser> GetApplicationUserWithDefaultGraphMapFromClaimsPrincipalUser(System.Security.Claims.ClaimsPrincipal userPrincipal)
        {
            var identityUser = await identityUserManager.GetUserAsync(userPrincipal);

            var user = await db.ApplicationUsers.Where(u => u.UserName == identityUser.UserName).FirstOrDefaultAsync();
            user.Map = db.GraphMaps.Where(m => m.IsDefault).FirstOrDefault();
            user.Map.Graph = new Graph
            {
                Id = user.Map.Id,
                Name = user.Map.Name,
                GraphString = user.Map.GraphString
            };
            ///TODO: Iitialize user.Map.Graph.Map Dictionary with Name, Node
            var lines = Regex.Split(user.Map.Graph.GraphString, @"(?=[*#])").Where(line => !string.IsNullOrEmpty(line)).ToArray();
            foreach (var line in lines)
            {
                var tempLine = line.Substring(1, line.Length - 1);
                switch (line[0])
                {
                    case '*':
                        Node node;
                        if (tempLine.Contains('('))
                        {
                            var nodeParts = tempLine.Split("(", 2);
                            node = new Node(nodeParts[0], nodeParts[1].Remove(nodeParts[1].Length - 1));
                        }
                        else
                        {
                            node = new Node(tempLine);
                        }

                        user.Map.Graph.AddNode(node);
                        break;
                    case '#':
                        var edgeParts = tempLine.Split("(", 2);
                        var nodes = edgeParts[0].Split(edgeParts.Contains("<->") ? "<->" : "->");
                        ///TODO: Edge string -> Graph add edge
                        var values = edgeParts[1].Remove(edgeParts[1].Length - 1)
                            .Split(',').Select(value => value.Trim())
                            .Where(value => !string.IsNullOrWhiteSpace(value)).ToArray();

                        user.Map.Graph.AddEdge(nodes[0], nodes[1], double.Parse(values[0]), TimeSpan.FromTicks(long.Parse(values[1])), double.Parse(values[2]), byte.Parse(values[3]), null);
                        break;
                }
            }

            return user;
        }
    }
}