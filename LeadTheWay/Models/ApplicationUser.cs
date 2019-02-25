using LeadTheWay.Models.GraphLayer;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeadTheWay.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "User")]
        public string Name { get; set; }

        public string GraphString { get; set; }

        [NotMapped]
        public Graph Graph { get; set; }

        [NotMapped]
        public bool IsAdmin { get; set; }


        //public ApplicationUser(string name, string graphString) : base()
        //{
        //    Name = name;
        //    GraphString = graphString;

        //    Graph = new Graph(); ///TODO: Graph = Utility.StringToGraph(GraphString); //от стринг създава целия graph съдържащ Map-a от тип Dictionary
        //    //IsAdmin = ?
        //}
    }
}
