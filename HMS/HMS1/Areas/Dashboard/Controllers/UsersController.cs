using HMS.Entities;
using HMS.Services;
using HMS1.Areas.Dashboard.Models.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HMS1.Areas.Dashboard.Controllers
{
    public class UsersController : Controller
    {


        private HMSSignInManager _signInManager;
        private HMSUserManager _userManager;
        private HMSRoleManager _roleManager;



        public UsersController()
        {
        }

        public UsersController(HMSUserManager userManager, HMSSignInManager signInManager, HMSRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        public HMSSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<HMSSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public HMSRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<HMSRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        public HMSUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<HMSUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


     


        public async Task<ActionResult> Index(string searchTerm, string RoleID, int? page)
        {
            int recordSize = 8;
            page = page ?? 1;

            UsersListModel model = new UsersListModel();
            model.SearchTerm = searchTerm;
            model.RoleID = RoleID;
            model.Roles = RoleManager.Roles.ToList();
         

            model.Users =  await SearchUsers(searchTerm, RoleID, page.Value, recordSize);

            var TotalRecords = await SearchUsersCount(searchTerm, RoleID);

            model.Pager = new Pager(TotalRecords, page, recordSize);

            return View(model);
        }




        [HttpGet]
        public async Task<ActionResult>  Action(string ID)
        {

            UsersActionModel model = new UsersActionModel();

            if (!string.IsNullOrEmpty(ID))// edit
            {
                var users = await UserManager.FindByIdAsync(ID);

                model.ID = users.Id;
                model.FullName = users.FullName;

                model.Email = users.Email;
                model.Country = users.Country;
                model.Address = users.Address;
                model.City = users.City;
                model.username = users.UserName;

            }

            // model.Roles = PackageServices.GetAllAccomodationPackage();

            return PartialView("_Action", model);
        }

        [HttpPost]
        public async Task<ActionResult> Action(UsersActionModel model)
        {
            JsonResult json = new JsonResult();

            IdentityResult result = null;

            if (!string.IsNullOrEmpty(model.ID)) // tyring to edit model
            {
                var users = await UserManager.FindByIdAsync(model.ID);

             
                model.FullName = users.FullName;

                model.Email = users.Email;
                model.Country = users.Country;
                model.Address = users.Address;
                model.City = users.City;
                model.username = users.UserName;


                 result = await UserManager.UpdateAsync(users);
                json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors)  };

            }
            else // create the record
            {
                var users = new HMSUser();

                users.FullName = model.FullName ;
               
                  users.Email = model.Email;
                  users.Country = model.Country;
                  users.Address = model.Address;
                  users.City = model.City;
                 users.UserName = model.username;

                result = await UserManager.CreateAsync(users);

            }




            json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };



            return json;
        }

        [HttpGet]
        public async Task<ActionResult>  Delete(string ID)
        {

            UsersActionModel model = new UsersActionModel();

            var user = await UserManager.FindByIdAsync(ID);

            model.ID = user.Id;


            return PartialView("_Delete", model);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(UsersActionModel model)
        {
            JsonResult json = new JsonResult();



            IdentityResult result = null;

            if (!string.IsNullOrEmpty(model.ID)) // tyring to delete record
            {
                var user = await UserManager.FindByIdAsync(model.ID);

                result = await UserManager.DeleteAsync(user);

                json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };
            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid User" };
            }

            


            return json;
            }
        [HttpGet]
        public async Task<ActionResult> UserRoles(string ID)
        {
            var user = await UserManager.FindByIdAsync(ID);

            UsersRoleModel model = new UsersRoleModel();
    
            model.userId = ID;
            var userRolesId = user.Roles.Select(x => x.RoleId).ToList();
            model.UserRoles = RoleManager.Roles.Where(x => userRolesId.Contains(x.Id)).ToList();

            model.Roles = RoleManager.Roles.Where(x => !userRolesId.Contains(x.Id)).ToList();


            return PartialView("_UserRoles", model);
        }

        [HttpPost]
        public async Task<JsonResult> UserRoleOperation( string userId, string roleId, bool isDelete = false)
        {
            JsonResult json = new JsonResult();

            IdentityResult result = null;

            var user = await UserManager.FindByIdAsync(userId);
            var role = await RoleManager.FindByIdAsync(roleId);

            if (user != null && role != null)
            {
                if (!isDelete)
                {
                     result = await UserManager.AddToRoleAsync(userId, role.Name);
                }
                else 
                {
                     result = await UserManager.RemoveFromRoleAsync(userId, role.Name);
                }

                    json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };
            }

            else
            {
                json.Data = new { Success = false, Message = "Invalid Operations" };
            }
            

            return json;
        }


        public async Task<IEnumerable<HMSUser>> SearchUsers(string searchTerm, string roleID, int page, int recordSize)
        {

            var users = UserManager.Users.AsQueryable();



            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(a => a.Email.ToLower().StartsWith(searchTerm.ToLower()));
            }
            if (!string.IsNullOrEmpty(roleID))
            {
               // users = users.Where(x => x.Roles.Select(y => y.RoleId).Contains(roleID));

                var role = await RoleManager.FindByIdAsync(roleID);
                var userIDs = role.Users.Select(x => x.UserId).ToList();

                users = users.Where(x => userIDs.Contains(x.Id));
            }


            var skip = (page - 1) * recordSize;

            return users.OrderBy(x => x.Email).Skip(skip).Take(recordSize).ToList();
        }

        public async Task<int> SearchUsersCount(string searchTerm, string roleId)
        {

            var users = UserManager.Users.AsQueryable();



            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(a => a.Email.ToLower().StartsWith(searchTerm.ToLower()));
            }

            if (!string.IsNullOrEmpty(roleId))
            {
                var role = await RoleManager.FindByIdAsync(roleId);
                var userIDs = role.Users.Select(x => x.UserId).ToList();

                users = users.Where(x => userIDs.Contains(x.Id));
            }



            return users.Count();
        }

    }
}