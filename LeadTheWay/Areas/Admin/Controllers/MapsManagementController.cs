using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LeadTheWay.Areas.Admin.Models.DTOs;
using LeadTheWay.Data;
using LeadTheWay.GraphLayer.Link.Domain.Models;
using LeadTheWay.GraphLayer.Map.Domain.Models;
using LeadTheWay.GraphLayer.Map.Service;
using LeadTheWay.GraphLayer.Vertex.Service;
using LeadTheWay.Models.ViewModels;
using LeadTheWay.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeadTheWay.Areas.Admin.Controllers
{
    [Authorize(Roles = StaticDetails.AdminUser)]
    [Area("Admin")]
    public class MapsManagementController : Controller
    {
        private readonly ApplicationDbContext db;

        [BindProperty]
        public GraphMapViewModel GraphMapVM { get; set; }


        public MapsManagementController(ApplicationDbContext db)
        {
            this.db = db;

            GraphMapVM = new GraphMapViewModel()
            {
                GraphMap = new GraphMap(),
                Edges = db.IntercityLinks.ToList()
            };
            GraphMapVM.GraphMap.Graph = new Graph();
            //GraphMapVM.GraphMap.CurrentEdgeIdToAdd = 0; ///TODO: test?
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

        // GET: MapsManagement/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

            //test
            graph.GraphString = graph.GraphString ?? "";
            graph.Graph = graph.Graph ?? new Graph();
            graph.NodeHistoryString = graph.NodeHistoryString ?? "";
            graph.NodeHistory = graph.NodeHistory ?? new List<string>();
            graph.EdgeHistoryString = graph.EdgeHistoryString ?? "";
            graph.EdgeHistory = graph.EdgeHistory ?? new List<string>();
            //end of test

            GraphMapVM.GraphMap = graph;

            return View(GraphMapVM);
        }

        // POST: MapsManagement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != GraphMapVM.GraphMap.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var edge = await db.IntercityLinks.Where(e => e.Id == GraphMapVM.GraphMap.CurrentEdgeIdToAdd).FirstOrDefaultAsync();

                bool isNotDirected = edge.EdgeString.Contains("<->");
                var nodes = isNotDirected ? edge.EdgeString.Split(new string[] { "<->" }, StringSplitOptions.None) : edge.EdgeString.Split(new string[] { "->" }, StringSplitOptions.None);
                edge.NodesPair = new VerticesPair
                {
                    FirstNodeId = (await db.TransportVertices.Where(n => n.Name == nodes[0]).FirstOrDefaultAsync()).Id,
                    RelatedNodeId = (await db.TransportVertices.Where(n => n.Name == nodes[1]).FirstOrDefaultAsync()).Id
                };

                var firstNode = await db.TransportVertices.Where(n => n.Id == edge.NodesPair.FirstNodeId).FirstOrDefaultAsync();
                var relatedNode = await db.TransportVertices.Where(n => n.Id == edge.NodesPair.RelatedNodeId).FirstOrDefaultAsync();

                var graph = GraphMapVM.GraphMap;

                graph.GraphString = graph.GraphString ?? "";
                graph.Graph = graph.Graph ?? new Graph();
                graph.NodeHistoryString = graph.NodeHistoryString ?? "";
                graph.NodeHistory = graph.NodeHistory ?? new List<string>();
                graph.EdgeHistoryString = graph.EdgeHistoryString ?? "";
                graph.EdgeHistory = graph.EdgeHistory ?? new List<string>();


                if (!GraphMapVM.GraphMap.NodeHistory.Exists(n => string.Equals(n, firstNode.Name)))
                {
                    bool isEmpty = GraphMapVM.GraphMap.NodeHistory.Count == 0;
                    GraphMapVM.GraphMap.NodeHistory.Add(firstNode.Name);
                    string appendixString = isEmpty ? firstNode.Name : $", {firstNode.Name}";
                    GraphMapVM.GraphMap.NodeHistoryString += appendixString;

                    GraphMapVM.GraphMap.Graph.AddNode(new Node(firstNode.Name, firstNode.Description));
                }
                if (!GraphMapVM.GraphMap.NodeHistory.Exists(n => string.Equals(n, relatedNode.Name)))
                {
                    bool isEmpty = GraphMapVM.GraphMap.NodeHistory.Count == 0;
                    GraphMapVM.GraphMap.NodeHistory.Add(relatedNode.Name);
                    string appendixString = isEmpty ? relatedNode.Name : $", {relatedNode.Name}";
                    GraphMapVM.GraphMap.NodeHistoryString += appendixString;

                    GraphMapVM.GraphMap.Graph.AddNode(new Node(relatedNode.Name, relatedNode.Description));
                }

                if (!GraphMapVM.GraphMap.EdgeHistory.Exists(e => string.Equals(e, edge.EdgeString))) ///TODO: Update direction only if edge exists with different direction (isNotDirected flag)
                {
                    bool isEmpty = GraphMapVM.GraphMap.EdgeHistory.Count == 0;
                    GraphMapVM.GraphMap.EdgeHistory.Add(edge.EdgeString);
                    string appendixString = isEmpty ? edge.EdgeString : $", {edge.EdgeString}";
                    GraphMapVM.GraphMap.EdgeHistoryString += appendixString;
                }

                ///TODO: create EdgeHistoryString from EdgeHistory then create the GraphString and update
                if (isNotDirected)
                {
                    GraphMapVM.GraphMap.Graph.AddBidirectionalEdge(firstNode.Name, relatedNode.Name, edge.Length, TimeSpan.FromTicks(edge.DurationTicks), edge.Price, edge.ServiceClass, null); ///TODO TimetableString to Timetable object
                }
                else
                {
                    GraphMapVM.GraphMap.Graph.AddEdge(firstNode.Name, relatedNode.Name, edge.Length, TimeSpan.FromTicks(edge.DurationTicks), edge.Price, edge.ServiceClass, null); ///TODO TimetableString to Timetable object
                }

                if (!GraphMapVM.GraphMap.Graph.Err)
                {
                    var mapDB = await db.GraphMaps.Where(m => m.Name == GraphMapVM.GraphMap.Name).FirstOrDefaultAsync();

                    ///TODO: comma between old string from db and new string added to the db (but working without it)
                    mapDB.NodeHistoryString = mapDB.NodeHistoryString ?? "";

                    var vertices = Regex.Replace(GraphMapVM.GraphMap.NodeHistoryString, @"\s+", "").Split(',');
                    if (!vertices.Any(mapDB.NodeHistoryString.Contains))
                    {
                        mapDB.NodeHistoryString += string.IsNullOrWhiteSpace(mapDB.NodeHistoryString) ? GraphMapVM.GraphMap.NodeHistoryString : $", { GraphMapVM.GraphMap.NodeHistoryString }";
                    }
                    else if (!vertices.All(mapDB.NodeHistoryString.Contains))
                    {
                        foreach (var vertex in vertices)
                        {
                            if (!mapDB.NodeHistoryString.Contains(vertex))
                            {
                                mapDB.NodeHistoryString += $", {vertex}";
                            }
                        }
                    }

                    mapDB.EdgeHistoryString += string.IsNullOrWhiteSpace(mapDB.EdgeHistoryString) ? GraphMapVM.GraphMap.EdgeHistoryString : $", {GraphMapVM.GraphMap.EdgeHistoryString}";

                    ///                    //
                    //tests
                    ///TODO: GraphString = Graph to string
                    string ns = "";
                    //foreach (var kvp in GraphMapVM.GraphMap.Graph.Map)
                    //{
                    //    ns += $"*{kvp.Key}({kvp.Value.Description})";
                    //}
                    foreach (var nodeName in Regex.Replace(mapDB.NodeHistoryString, @"\s+", "").Split(','))
                    {
                        if (GraphMapVM.GraphMap.Graph.Map.TryGetValue(nodeName, out var node))
                        {
                            ns += $"*{nodeName}({node.Description})";
                        }
                    }

                    string es = "";
                    //foreach (var item in GraphMapVM.GraphMap.EdgeHistory)
                    //{
                    //    var tempEdge = await db.IntercityLinks.Where(e => e.EdgeString == item).FirstOrDefaultAsync();
                    //    es += $"#{item}({tempEdge.Length}, {tempEdge.DurationTicks}, {tempEdge.Price}, {tempEdge.ServiceClass})";
                    //}
                    foreach (var item in GraphMapVM.GraphMap.EdgeHistory)
                    {
                        var tempEdge = await db.IntercityLinks.Where(e => e.EdgeString == item).FirstOrDefaultAsync();
                        es += $"#{item}({tempEdge.Length}, {tempEdge.DurationTicks}, {tempEdge.Price}, {tempEdge.ServiceClass})";
                    }
                    string gstring = ns + es;
                    ///TODO: Fix it!
                    mapDB.GraphString = mapDB.GraphString ?? "";
                    GraphMapVM.GraphMap.GraphString = gstring;
                    //end of tests
                    //
                    mapDB.GraphString += GraphMapVM.GraphMap.GraphString;



                    ///TODO: Duplicating all Nodes when adding Edges to DB (possible solution: check db.nodehistory, db.edgehistory, not graphmapvm.nodehistory and not graphmapvm.edgehistory
                    //var graphStringLines = mapDB.GraphString.Split(new[] { "*", "#" }, StringSplitOptions.RemoveEmptyEntries);
                    mapDB.GraphString = string.Join("", Regex.Split(mapDB.GraphString, @"(?=[*#])").Distinct().ToList());


                    //db.Update(GraphMapVM.GraphMap);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return NotFound(); ///TODO: Better approach
                }
                //db.Update(GraphMapVM.GraphMap);
                //await db.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            return View(GraphMapVM);
        }

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