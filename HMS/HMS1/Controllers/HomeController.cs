using HMS.Services;
using HMS1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS1.Controllers
{
    public class HomeController : Controller
    {
        AccomodationtypesServices typeService = new AccomodationtypesServices();
        AccomodationPackageServices packageservices = new AccomodationPackageServices();

        public ActionResult Index()
        {
            HomeViewModels model = new HomeViewModels();

            model.AccomodationTypes = typeService.GetAllAccomodationTypes();

            model.AccomodationPackages = packageservices.GetAllAccomodationPackage();

            return View(model);
        }

        public ActionResult About()
        {
           

            return View();
        }

        public ActionResult Contact()
        {
           

            return View();
        }
    }
}