using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS1.Models
{
    public class HomeViewModels
    {
        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }

        public IEnumerable<AccomodationPackagee> AccomodationPackages { get; set; }
    }
}