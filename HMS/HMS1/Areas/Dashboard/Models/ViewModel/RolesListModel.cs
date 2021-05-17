using HMS.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS1.Areas.Dashboard.Models.ViewModel
{
    
    
        public class RolesListModel
        {


          
            public string SearchTerm { get; set; }
            public IEnumerable<IdentityRole> Roles { get; set; }
            public Pager Pager { get; set; }
        }



        public class RolesActionModel
        {



        public string ID { get; set; }
        public string Name { get; set; }


        //public IEnumerable<IdentityRole> Roles { get; set; }

    }

    
}