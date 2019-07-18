using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PVA.Web.Data;
using PVA.Web.Models;
using PVA.Web.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PVA.Web.Controllers
{
    public class HomeController : BaseController
    {
        #region private variables

        private readonly GenericRepository<FARToBeVerified> _dbRepositoryFAR;
        private readonly GenericRepository<PlantAreaMaster> _dbRepositoryPlant;

        #endregion

        #region Constructor
        public HomeController()
        {
            _dbRepositoryFAR = new GenericRepository<FARToBeVerified>();
            _dbRepositoryPlant = new GenericRepository<PlantAreaMaster>();
        }
        #endregion

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult KendoReadPlant([DataSourceRequest] DataSourceRequest request)
        {
            if (!request.Sorts.Any())
            {
                request.Sorts.Add(new SortDescriptor("PLANT_NAME", ListSortDirection.Ascending));
            }
            return Json(_dbRepositoryPlant.GetEntities().ToDataSourceResult(request));
        }

        public ActionResult KendoReadFAR([DataSourceRequest] DataSourceRequest request, string plantid = null)
        {
            if (!request.Sorts.Any())
            {
                request.Sorts.Add(new SortDescriptor("AssetIDSubNo", ListSortDirection.Ascending));
            }

            if (!string.IsNullOrEmpty(plantid))
            {
                return Json(_dbRepositoryFAR.GetEntities().Where(m => m.PlantID == plantid).ToDataSourceResult(request));
            }
            else
            {
                return Json(_dbRepositoryFAR.GetEntities().ToDataSourceResult(request));
            }
        }

        public PartialViewResult ShowPreview(string PlantId, string FARId)
        {
            FARImageModel obj = new FARImageModel();
            if (!string.IsNullOrEmpty(PlantId) && !string.IsNullOrEmpty(FARId))
            {
                PlantAreaMaster master = _dbRepositoryPlant.SelectById(PlantId);
                FARToBeVerified masterFAR = _dbRepositoryFAR.SelectById(FARId);
                if (master != null && masterFAR != null)
                {
                    obj.PlantName = master.PLANT;
                    obj.ImageName = masterFAR.AssetImage;
                    if (!string.IsNullOrEmpty(masterFAR.AssetImage))
                    {
                        string imagePath = string.Format("http://pvapplicationnew.centralus.cloudapp.azure.com/Images/{0}/ComponentImage/{1}", master.PLANT, masterFAR.AssetImage);
                        obj.ImagePath = imagePath;
                    }
                }
            }

            return PartialView("_Preview", obj);
        }

        public ActionResult DownloadImage(string path)
        {
            try
            {
                if (!string.IsNullOrEmpty(path))
                {
                    //string fileExtension = Path.GetExtension(path);
                    //string filePath = Server.MapPath(path);
                    //byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                    //var response = new FileContentResult(fileBytes, "image/jpeg");
                    //response.FileDownloadName = "Image.jpeg";
                    //return response;

                    using (System.Net.WebClient client = new System.Net.WebClient())
                    {
                        byte[] fileBytes = client.DownloadData(path);
                        var response = new FileContentResult(fileBytes, "image/jpeg");
                        response.FileDownloadName = "Image.jpeg";
                        return response;
                    }

                }
                else
                {
                    string link = Url.Action("Index", "Home");
                    return Content("<h4>File not found</h4> <br/><br/> <a href=" + link + "> Back To Site </a>");
                }
            }
            catch (Exception ex)
            {
                string link = $"http://{Request.Url.Authority}/Home/Index";
                //var link = Url.Action("Index", "Home");
                return Content("<h4>File not found</h4> <br/><br/> <a href="+ link + "> Back To Site </a>");
            }
        }
    }
}
