using HMS.Entities;
using HMS.Services;
using HMS1.Areas.Dashboard.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS1.Areas.Dashboard.Controllers
{
    public class AccomodationTypesController : Controller
    {

        AccomodationtypesServices AccoServices = new AccomodationtypesServices();

        public ActionResult Index(string searchTerm)
        {

            AccomodationTypesListModel model = new AccomodationTypesListModel();

            model.AccomodationTypes = AccoServices.SearchAccomodationTypes(searchTerm);
            model.SearchTerm = searchTerm;


            return View(model);
        }


       

        [HttpGet]
        public ActionResult Action(int? ID)
        {

            AccomodationTypesActionModel model = new AccomodationTypesActionModel();

            if (ID.HasValue)// edit
            {
                var accomodationType = AccoServices.GetAllAccomodationTypesByID(ID.Value);
                  model.ID = accomodationType.ID;
                  model.Name = accomodationType.Name;
               model.Description = accomodationType.Description;
            }
          

            return PartialView("_Action", model);
        }

        [HttpPost]
        public JsonResult Action(AccomodationTypesActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;
            if(model.ID > 0) // tyring to edit model
            {
                var accomType = AccoServices.GetAllAccomodationTypesByID(model.ID);

                accomType.Name = model.Name;
                accomType.Description = model.Description;

                result = AccoServices.UpdateAccomodationType(accomType);
            }
            else // create the record
            {
                AccomodationType accomodationType = new AccomodationType();
                accomodationType.Name = model.Name;
                accomodationType.Description = model.Description;

                 result = AccoServices.SaveAccomodationType(accomodationType);

            } 

             

            

            if (result)
            {

                json.Data = new {Success = true };
            }
            else
            {

                json.Data = new {Success = false , Message = "Unable to perform action on  Accomodation Type" };
            }



            return json;
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {

            AccomodationTypesActionModel model = new AccomodationTypesActionModel();

            var accomodationType = AccoServices.GetAllAccomodationTypesByID(ID);
                
                model.ID = accomodationType.ID;
              
             


            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(AccomodationTypesActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;


            var accomodationType = AccoServices.GetAllAccomodationTypesByID(model.ID);

            result = AccoServices.DeleteAccomodationType(accomodationType);




            if (result)
            {

                json.Data = new { Success = true };
            }
            else
            {

                json.Data = new { Success = false, Message = "Unable to perform action on  Accomodation Type" };
            }



            return json;
        }

    }
}