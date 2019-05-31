using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LeadTheWay.Data;
using LeadTheWay.GraphLayer.Map.Service;
using LeadTheWay.GraphLayer.Map.Service.Search.Services;
using LeadTheWay.GraphLayer.Map.Service.Search.Services.Utility;
using LeadTheWay.GraphLayer.Vertex.Service;
using LeadTheWay.Models;
using LeadTheWay.Models.ViewModels;
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

        [BindProperty]
        public MapServiceViewModel MapVM { get; set; }

        public MapController(ApplicationDbContext db, UserManager<IdentityUser> identityUserManager)
        {
            this.db = db;
            this.identityUserManager = identityUserManager;
            MapVM = new MapServiceViewModel();
        }

        // GET: Map
        public async Task<IActionResult> Index()
        {
            ///TODO:
            //var result = Graph.Search(new CheapestPathSearch(user.Map.Graph), "Plovdiv", "Varna");
            //var cheapest = GraphUtil.ResultCheapest;
            //cheapest += result + '\n';

            MapVM.User = await GetApplicationUserWithDefaultGraphMapFromClaimsPrincipalUser(User);

            ///TODO: To check if the departure/arrival names are set and to display the algorithm results

            return View(MapVM);
        }

        // POST: Map
        [HttpPost, ActionName("Index")]
        public async Task<IActionResult> IndexPost()
        {
            MapVM.User = await GetApplicationUserWithDefaultGraphMapFromClaimsPrincipalUser(User);

            string result = string.Empty;
            if (!string.IsNullOrEmpty(MapVM.IntermediatePlace))
            {
                result = Graph.Search(
                    new PathWithIntermediatePointsSearch(MapVM.User.Map.Graph),
                    MapVM.DeparturePlace,
                    MapVM.ArrivalPlace,
                    new List<Node>() { MapVM.User.Map.Graph.GetNodeByName(MapVM.IntermediatePlace) });
            }
            else if (MapVM.DeparturePlace != null && MapVM.ArrivalPlace != null)
            {
                result = Graph.Search(new CheapestPathSearch(MapVM.User.Map.Graph), MapVM.DeparturePlace, MapVM.ArrivalPlace);
            }

            GraphUtil.ResultCheapest += result;
            MapVM.Path = GraphUtil.ResultCheapest;

            //var region = new List<string>() { "Varna", "Plovdiv|" };
            //int from = MapVM.Path.IndexOf(region[0]) + region[0].Length;
            //int to = MapVM.Path.LastIndexOf(region[1]);
            //MapVM.PathShort = MapVM.Path.Substring(from, to - from);

            var sub = MapVM.Path.Substring(MapVM.Path.LastIndexOf("Cost"));
            sub = sub.Substring(sub.IndexOf("\n") + "\n".Length).Split("\n".ToCharArray()).First();
            sub = sub.Remove(sub.Length - 1);
            var split = sub.Split(new string[] { "<-" }, StringSplitOptions.None).ToList();
            split.Reverse();
            MapVM.PathShort = string.Join("->", split.ToArray());
            var stations = new List<Node>();
            foreach (var townName in split)
            {
                stations.Add(MapVM.User.Map.Graph.GetNodeByName(townName));
            }

            double sum = 0;
            for (int i = 0; i < stations.Count - 1; i++)
            {
                foreach (var link in stations[i].Edges)
                {
                    if (link.RelatedNode.Name.Equals(stations[i + 1].Name))
                    {
                        sum += link.Price;
                    }
                }
            }

            MapVM.PathPrice = Math.Round(sum, 2);
            MapVM.PathLength = stations[stations.Count - 1].CurrentLength;

            return View(MapVM);
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