using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entities
{
   public class Accomodation
    {
        public int ID { get; set; }

        public int AccomodationPackageeID { get; set; }

        public virtual AccomodationPackagee AccomodationPackagee { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public List<AccomodationPicture> AccomodationPictures { get; set; }
    }
}
