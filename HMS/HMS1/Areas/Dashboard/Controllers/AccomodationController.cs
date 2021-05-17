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
    public class AccomodationController : Controller
    {

        

            AccomodationPackageServices PackageServices = new AccomodationPackageServices();
            AccomodationServices accomodatioService = new AccomodationServices();


            public ActionResult Index(string searchTerm, int? AccomodationPackageeID, int? page)
            {
                int recordSize = 7;
                page = page ?? 1;

                AccomodationListModel model = new AccomodationListModel();
                model.SearchTerm = searchTerm;
                model.AccomodationPackageeID = AccomodationPackageeID;
                model.AccomodationPackages = PackageServices.GetAllAccomodationPackage();

                model.Accomodations = accomodatioService.SearchAccomodation(searchTerm, AccomodationPackageeID, page.Value, recordSize);

                var TotalRecords = accomodatioService.SearchAccomodationCount(searchTerm, AccomodationPackageeID);

                model.Pager = new Pager(TotalRecords, page, recordSize);

                return View(model);
            }




            [HttpGet]
            public ActionResult Action(int? ID)
            {

                AccomodationActionModel model = new AccomodationActionModel();

                if (ID.HasValue)// edit
                {
                    var accomodation = accomodatioService.GetAllAccomodationByID(ID.Value);

                    model.ID = accomodation.ID;
                    model.Name = accomodation.Name;
                   
                    model.Description = accomodation.Description;
                   model.AccomodationPackageID = accomodation.AccomodationPackageeID;
                }

                model.AccomodationPackages = PackageServices.GetAllAccomodationPackage();

                return PartialView("_Action", model);
            }

            [HttpPost]
            public JsonResult Action(AccomodationActionModel model)
            {
                JsonResult json = new JsonResult();

                var result = false;
                if (model.ID > 0) // tyring to edit model
                {
                    var accomPack = accomodatioService.GetAllAccomodationByID(model.ID);

                    accomPack.AccomodationPackageeID = model.AccomodationPackageID;
                    accomPack.Name = model.Name;
                    accomPack.Description = model.Description;
                    

                    result = accomodatioService.UpdateAccomodation(accomPack);
                }
                else // create the record
                {
                    Accomodation accomodation = new Accomodation();
                    accomodation.AccomodationPackageeID = model.AccomodationPackageID;
                    accomodation.Name = model.Name;
                    accomodation.Description = model.Description;
                    

                    result = accomodatioService.SaveAccomodation(accomodation);

                }





                if (result)
                {

                    json.Data = new { Success = true };
                }
                else
                {

                    json.Data = new { Success = false, Message = "Unable to perform action on  Accomodation Package" };
                }



                return json;
            }

            [HttpGet]
            public ActionResult Delete(int ID)
            {

                AccomodationActionModel model = new AccomodationActionModel();

                var accomodation = accomodatioService.GetAllAccomodationByID(ID);

                model.ID = accomodation.ID;


                return PartialView("_Delete", model);
            }

            [HttpPost]
            public JsonResult Delete(AccomodationActionModel model)
            {
                JsonResult json = new JsonResult();

                var result = false;


                var accomodation = accomodatioService.GetAllAccomodationByID(model.ID);

                result = accomodatioService.DeleteAccomodation(accomodation);




                if (result)
                {

                    json.Data = new { Success = true };
                }
                else
                {

                    json.Data = new { Success = false, Message = "Unable to perform action on  Accomodation" };
                }



                return json;
            }

        }

    
}