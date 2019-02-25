using System;
using System.Collections.Generic;
using System.Text;
using LeadTheWay.Models;
using LeadTheWay.Models.GraphLayer.GraphLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeadTheWay.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<GraphMap> GraphMaps { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
