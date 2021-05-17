using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.Entities;
using HMS.Services;
using HMS1.Areas.Dashboard.Models.ViewModel;

namespace HMS1.Areas.Dashboard.Controllers
{
    public class AccomodationPackageController : Controller
    {

        AccomodationPackageServices PackageServices = new AccomodationPackageServices();
        AccomodationtypesServices AccoServices = new AccomodationtypesServices();
        DashboardServices dashboardService = new DashboardServices();

        public ActionResult Index(string searchTerm, int? accomodationTypeID, int? page)
        {
            int recordSize = 8;
            page = page ?? 1;

            AccomodationPackageListModel model = new AccomodationPackageListModel();
            model.SearchTerm = searchTerm;
            model.AccomodationTypeID = accomodationTypeID;
            model.AccomodationTypes = AccoServices.GetAllAccomodationTypes();

            model.AccomodationPackages = PackageServices.SearchAccomodationPackage(searchTerm, accomodationTypeID,page.Value,recordSize);

            var TotalRecords = PackageServices.SearchAccomodationPackageCount(searchTerm, accomodationTypeID);

            model.Pager = new Pager(TotalRecords, page,recordSize);

            return View(model);
        }




        [HttpGet]
        public ActionResult Action(int? ID)
        {

            AccomodationPackageActionModel model = new AccomodationPackageActionModel();

            if (ID.HasValue)// edit
            {
                var accomodationPackage = PackageServices.GetAllAccomodationPackagesByID(ID.Value);

                model.ID = accomodationPackage.ID;
                model.AccomodationTypeId = accomodationPackage.AccomodationTypeId;
                model.Name = accomodationPackage.Name;
                model.NoOfRoom = accomodationPackage.NoOfRoom;
                model.FeePerNight = accomodationPackage.FeePerNight;
                model.AccomodationPackagePictures = PackageServices.GetPicturesByAccomodationPackageID(accomodationPackage.ID);
                //model.AccomodationPackagePictures = accomodationPackage.AccomodationPackagePictures;
                
            }

            model.AccomodationTypes = AccoServices.GetAllAccomodationTypes();

            return PartialView("_Action", model);
        }


        [HttpPost]
        public JsonResult Action(AccomodationPackageActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;
            var list = model.PictureIDs;
            var picIDs = string.Join(",", list);

            List<int> pictureIDs = !string.IsNullOrEmpty(picIDs) ? model.PictureIDs.Select(x => int.Parse(x)).ToList() : new List<int>();

            var pictures = dashboardService.GetPicturesByIDs(pictureIDs);

            if (model.ID > 0) // tyring to edit model
            {
                var accomodationPackage = PackageServices.GetAllAccomodationPackagesByID(model.ID);

                accomodationPackage.ID = model.ID;
                accomodationPackage.AccomodationTypeId = model.AccomodationTypeId;
                accomodationPackage.Name = model.Name;
                accomodationPackage.NoOfRoom = model.NoOfRoom;
                accomodationPackage.FeePerNight = model.FeePerNight;

              

                accomodationPackage.AccomodationPackagePictures.Clear();

                accomodationPackage.AccomodationPackagePictures.AddRange(pictures.Select(x => new AccomodationPackagePicture() { AccomodationPackageID = accomodationPackage.ID , pictureID = x.ID}));
              

                result = PackageServices.UpdateAccomodationPackage(accomodationPackage);
            }
            else // create the record
            {
                AccomodationPackagee accomodationPackage = new AccomodationPackagee();
                accomodationPackage.AccomodationTypeId = model.AccomodationTypeId;
                accomodationPackage.Name = model.Name;
                accomodationPackage.NoOfRoom = model.NoOfRoom;
                accomodationPackage.FeePerNight = model.FeePerNight;


                accomodationPackage.AccomodationPackagePictures = new List<AccomodationPackagePicture>();
                accomodationPackage.AccomodationPackagePictures.AddRange(pictures.Select(x => new AccomodationPackagePicture() { pictureID = x.ID }));


                result = PackageServices.SaveAccomodationPackage(accomodationPackage);

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

            AccomodationPackageActionModel model = new AccomodationPackageActionModel();

            var package = PackageServices.GetAllAccomodationPackagesByID(ID);

            model.ID = package.ID;
            

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(AccomodationPackageActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;


            var accomodationPack = PackageServices.GetAllAccomodationPackagesByID(model.ID);

            result = PackageServices.DeleteAccomodationPackage(accomodationPack);




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

    }
}