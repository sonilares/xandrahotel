using HMS.Data;
using HMS.Entities;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
   public class AccomodationPackageServices
    {

        public IEnumerable<AccomodationPackagee> GetAllAccomodationPackage()
        {

            var context = new HMSContext();

            return context.AccomodationPackagees.ToList();
        }

        public IEnumerable<AccomodationPackagee> GetAllAccomodationPackageByAccomodationType(int accomodationTypeID)
        {

            var context = new HMSContext();

            return context.AccomodationPackagees.Where(x => x.AccomodationTypeId == accomodationTypeID).ToList();
        }

        public IEnumerable<AccomodationPackagee> SearchAccomodationPackage(string searchTerm, int? accomodationTypeID,int page, int recordSize)
        {

            var context = new HMSContext();

            var accomodationPackage = context.AccomodationPackagees.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodationPackage = accomodationPackage.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            if (accomodationTypeID.HasValue && accomodationTypeID.Value > 0)
            {
                accomodationPackage = accomodationPackage.Where(a => a.AccomodationTypeId == accomodationTypeID.Value);
            }

            var skip = ( page - 1) * recordSize;

            return accomodationPackage.OrderBy(x=>x.AccomodationTypeId).Skip(skip).Take(recordSize).ToList();
        }

        public int SearchAccomodationPackageCount(string searchTerm, int? accomodationTypeID)
        {

            var context = new HMSContext();

            var accomodationPackage = context.AccomodationPackagees.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodationPackage = accomodationPackage.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            if (accomodationTypeID.HasValue && accomodationTypeID.Value > 0)
            {
                accomodationPackage = accomodationPackage.Where(a => a.AccomodationTypeId == accomodationTypeID.Value);
            }

           

            return accomodationPackage.Count();
        }

        public AccomodationPackagee GetAllAccomodationPackagesByID(int ID)
        {
            var context = new HMSContext();
              
         return context.AccomodationPackagees.Find(ID);
 
        }


        public bool SaveAccomodationPackage(AccomodationPackagee accomodationPackagee)
        {


            var context = new HMSContext();

            context.AccomodationPackagees.Add(accomodationPackagee);

            return context.SaveChanges() > 0;
        }

        public bool UpdateAccomodationPackage(AccomodationPackagee accomodationPackagee)
        {

            var context = new HMSContext();

            var existingAccomodationPackage = context.AccomodationPackagees.Find(accomodationPackagee.ID);

            context.AccomodationPackagePictures.RemoveRange(existingAccomodationPackage.AccomodationPackagePictures);

            context.Entry(existingAccomodationPackage).CurrentValues.SetValues(accomodationPackagee);

            context.AccomodationPackagePictures.AddRange(accomodationPackagee.AccomodationPackagePictures);

            return context.SaveChanges() > 0;
        }

        public bool DeleteAccomodationPackage(AccomodationPackagee accomodationPackagee)
        {

            var context = new HMSContext();

            context.Entry(accomodationPackagee).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;
        }

        public List<AccomodationPackagePicture> GetPicturesByAccomodationPackageID(int accomodationPackageID)
        {

            var context = new HMSContext();

            return context.AccomodationPackagees.Find(accomodationPackageID).AccomodationPackagePictures.ToList();
        }

    }
}
