using HMS.Entities;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HMS.Services
{
   //public class UserServices
   // {

   //     private HMSSignInManager _signInManager;
   //     private HMSUserManager _userManager;


   //     public UserServices()
   //     {
   //     }

   //     public UserServices(HMSUserManager userManager, HMSSignInManager signInManager)
   //     {
   //         UserManager = userManager;
   //         SignInManager = signInManager;
   //     }

   //     public HMSSignInManager SignInManager
   //     {
   //         get
   //         {
   //             return _signInManager ?? HttpContext.GetOwinContext().Get<HMSSignInManager>();
   //         }
   //         private set
   //         {
   //             _signInManager = value;
   //         }
   //     }

   //     public HMSUserManager UserManager
   //     {
   //         get
   //         {
   //             return _userManager ?? HttpContext.GetOwinContext().GetUserManager<HMSUserManager>();
   //         }
   //         private set
   //         {
   //             _userManager = value;
   //         }
   //     }

   //     public IEnumerable<HMSUser> SearchUsers(string searchTerm, string roleID, int page, int recordSize)
   //     {

   //         var users = UserManager.Users.AsQueryable();

            

   //         if (!string.IsNullOrEmpty(searchTerm))
   //         {
   //             users = users.Where(a => a.Email.ToLower().StartsWith(searchTerm.ToLower()));
   //         }

           
   //         var skip = (page - 1) * recordSize;

   //         return users.OrderBy(x => x.Email).Skip(skip).Take(recordSize).ToList();
   //     }

   //     public int SearchUsersCount(string searchTerm, string roleId)
   //     {

   //         var users = UserManager.Users.AsQueryable();

           

   //         if (!string.IsNullOrEmpty(searchTerm))
   //         {
   //             users = users.Where(a => a.Email.ToLower().StartsWith(searchTerm.ToLower()));
   //         }

   //         if (!string.IsNullOrEmpty(roleId))
   //         {
   //            // accomodation = accomodation.Where(a => a.AccomodationPackageeID == accomodationPackageID.Value);
   //         }



   //         return users.Count();
   //     }
   // }
}
