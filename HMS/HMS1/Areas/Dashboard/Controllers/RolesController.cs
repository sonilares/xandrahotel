﻿using HMS.Entities;
using HMS.Services;
using HMS1.Areas.Dashboard.Models.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HMS1.Areas.Dashboard.Controllers
{
    public class RolesController : Controller
    {
        private HMSSignInManager _signInManager;
        private HMSUserManager _userManager;
        private HMSRoleManager _roleManager;

       
        public RolesController()
        {
        }

        public RolesController(HMSUserManager userManager, HMSSignInManager signInManager, HMSRoleManager roleManager)
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




        public ActionResult Index(string searchTerm, int? page)
        {
            int recordSize = 7;
            page = page ?? 1;

            RolesListModel model = new RolesListModel();
            model.SearchTerm = searchTerm;
           

            model.Roles = SearchRoles(searchTerm, page.Value, recordSize);

            var TotalRecords = SearchRolesCount(searchTerm);

            model.Pager = new Pager(TotalRecords, page, recordSize);

            return View(model);
        }




        [HttpGet]
        public async Task<ActionResult> Action(string ID)
        {

            RolesActionModel model = new RolesActionModel();

            if (!string.IsNullOrEmpty(ID))// edit
            {
                var role = await RoleManager.FindByIdAsync(ID);

                model.ID = role.Id;
                model.Name = role.Name;
               


            }

            

            return PartialView("_Action", model);
        }

        [HttpPost]
        public async Task<ActionResult> Action(RolesActionModel model)
        {
            JsonResult json = new JsonResult();

            IdentityResult result = null;

            if (!string.IsNullOrEmpty(model.ID)) // tyring to edit model
            {
                var role = await RoleManager.FindByIdAsync(model.ID);


                model.Name = role.Name;
               
                


                result = await RoleManager.UpdateAsync(role);
                json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };

            }
            else // create the record
            {
                var role = new IdentityRole();

                role.Name = model.Name;

              

                result = await RoleManager.CreateAsync(role);

            }




            json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };



            return json;
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string ID)
        {

            RolesActionModel model = new RolesActionModel();

            var role = await RoleManager.FindByIdAsync(ID);

            model.ID = role.Id;


            return PartialView("_Delete", model);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(RolesActionModel model)
        {
            JsonResult json = new JsonResult();



            IdentityResult result = null;

            if (!string.IsNullOrEmpty(model.ID)) // tyring to delete record
            {
                var role = await RoleManager.FindByIdAsync(model.ID);

                result = await RoleManager.DeleteAsync(role);

                json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };
            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid User" };
            }




            return json;
        }


        public IEnumerable<IdentityRole> SearchRoles(string searchTerm, int page, int recordSize)
        {

            var roles = RoleManager.Roles.AsQueryable();



            if (!string.IsNullOrEmpty(searchTerm))
            {
                roles = roles.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }


            var skip = (page - 1) * recordSize;

            return roles.OrderBy(x => x.Name).Skip(skip).Take(recordSize).ToList(); ;
        }

        public int SearchRolesCount(string searchTerm)
        {

            var roles = RoleManager.Roles.AsQueryable();



            if (!string.IsNullOrEmpty(searchTerm))
            {
                roles = roles.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            



            return roles.Count();
        }

    }
}