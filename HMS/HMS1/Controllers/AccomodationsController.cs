using HMS.Services;
using HMS1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS1.Controllers
{
    public class AccomodationsController : Controller
    {
        AccomodationtypesServices typeService = new AccomodationtypesServices();
        AccomodationPackageServices packageServices = new AccomodationPackageServices();
        AccomodationServices accoServices = new AccomodationServices();
        
        public ActionResult Index(int accomodationTypeID, int? accomodationPackageID)
        {
            AccomodationsViewModels model = new AccomodationsViewModels();

            model.accomodationType = typeService.GetAllAccomodationTypesByID(accomodationTypeID);

            model.accomodationPackages = packageServices.GetAllAccomodationPackageByAccomodationType(accomodationTypeID);

             model.selectedPackageID = accomodationPackageID.HasValue ? accomodationPackageID.Value : model.accomodationPackages.First().ID;

            model.accomodations = accoServices.GetAllAccomodationsByAccomodationPackage(model.selectedPackageID);

            return View(model);
        }


        public ActionResult Details(int accomodationPackageID)
        {
            AccomodationPackageViewModels model = new AccomodationPackageViewModels();
            model.accomodationPackage = packageServices.GetAllAccomodationPackagesByID(accomodationPackageID);
            

            return View(model);
        }
    }
}