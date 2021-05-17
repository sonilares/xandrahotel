using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS1.Models
{
    public class AccomodationsViewModels
    {
        public int selectedPackageID { get; set; }

        public AccomodationType accomodationType { get; set; }
        public IEnumerable<AccomodationPackagee> accomodationPackages { get; set; }
        public IEnumerable<Accomodation> accomodations { get; set; }

    }

    public class AccomodationPackageViewModels
    {
        public int selectedPackageID { get; set; }

        public AccomodationPackagee accomodationPackage { get; set; }
       

    }
}