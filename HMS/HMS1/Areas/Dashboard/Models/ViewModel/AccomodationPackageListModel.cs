using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS1.Areas.Dashboard.Models.ViewModel
{
   
    public class AccomodationPackageListModel
    {
        

        public IEnumerable<AccomodationPackagee> AccomodationPackages { get; set; }
        public string SearchTerm { get; set; }
        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }
        public int? AccomodationTypeID { get; set; }
        public Pager Pager { get; set; }
    }



    public class AccomodationPackageActionModel
    {

        public int ID { get; set; }
        public int AccomodationTypeId { get; set; }
        public AccomodationType AccomodationType { get; set; }
        public string Name { get; set; }
        public int NoOfRoom { get; set; }
        public decimal FeePerNight { get; set; }
   
        public List<string>  PictureIDs { get; set;}

        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }

        public List<AccomodationPackagePicture> AccomodationPackagePictures { get; set; }



    }


}