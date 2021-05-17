using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS1.Areas.Dashboard.Models.ViewModel
{
   
    public class AccomodationListModel
    {
        

        public IEnumerable<Accomodation> Accomodations { get; set; }
        public string SearchTerm { get; set; }
        public IEnumerable<AccomodationPackagee> AccomodationPackages { get; set; }
        public int? AccomodationPackageeID { get; set; }
        public Pager Pager { get; set; }
    }



    public class AccomodationActionModel
    {

        public int ID { get; set; }
        public int AccomodationPackageID { get; set; }
        public AccomodationPackagee AccomodationPackage { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<AccomodationPackagee> AccomodationPackages { get; set; }

    }


}