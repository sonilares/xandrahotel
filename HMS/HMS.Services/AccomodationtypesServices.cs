using HMS.Data;
using HMS.Entities;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace HMS.Services
{
    public class AccomodationtypesServices
    {

        public IEnumerable<AccomodationType> GetAllAccomodationTypes()
        {

            var context = new HMSContext();

            return context.AccomodationType.ToList();
        }
        public IEnumerable<AccomodationType> SearchAccomodationTypes(string searchTerm)
        { 

            var context = new HMSContext();

            var accomodationTypes = context.AccomodationType.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {

                accomodationTypes = accomodationTypes.Where(a => a.Name.ToLower().StartsWith(searchTerm.ToLower()
                    ));
            }

            return context.AccomodationType.ToList();
        }

        public AccomodationType GetAllAccomodationTypesByID(int ID)
        {

            var context = new HMSContext();

            return context.AccomodationType.Find(ID);
        }


        public bool SaveAccomodationType(AccomodationType accomodationType)
        {

            var context = new HMSContext();

             context.AccomodationType.Add(accomodationType);

            return context.SaveChanges() > 0;
        }

        public bool UpdateAccomodationType(AccomodationType accomodationType)
        {
 
            var context = new HMSContext();

            context.Entry(accomodationType).State = System.Data.Entity.EntityState.Modified; 

            return context.SaveChanges() > 0;
        }

        public bool DeleteAccomodationType(AccomodationType accomodationType)
        {

            var context = new HMSContext();

            context.Entry(accomodationType).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;
        }


    }
}
