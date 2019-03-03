using System;
using System.Collections.Generic;
using System.Text;
using LeadTheWay.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LeadTheWay.GraphLayer.Map.Domain.Models;
using LeadTheWay.GraphLayer.Link.Domain.Models;
using LeadTheWay.GraphLayer.Vertex.Domain.Models;

namespace LeadTheWay.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<GraphMap> GraphMaps { get; set; }

        public DbSet<TransportVertex> TransportVertices { get; set; }

        public DbSet<IntercityLink> IntercityLinks { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
