using HMS.Entities;
using HMS.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS1.Areas.Dashboard.Controllers
{
    public class DashboardController : Controller
    {
       
      
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UploadPictures()
        {
            JsonResult result = new JsonResult();
            var dashboardservice = new DashboardServices();
            var picList = new List<Picture>();

            var files = Request.Files;

            for (int i = 0; i < files.Count; i++)
            {
                var picture = files[i];

                //var pathToImagesFolder = Server.MapPath("images/site/");

                var fileName =  Guid.NewGuid() + Path.GetExtension(picture.FileName);

                var filePath = Server.MapPath("~/images/site/") + fileName;

                picture.SaveAs(filePath);

                var dbPicture = new Picture();

                dbPicture.URL = fileName;

                if (dashboardservice.SavePicture(dbPicture))
                {
                    picList.Add(dbPicture);
                }
            }

            result.Data = picList;

            return result;
        }

    }
}