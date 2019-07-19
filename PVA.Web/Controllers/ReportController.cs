using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PVA.Web.Data;
using PVA.Web.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PVA.Web.Controllers
{
    public class ReportController : BaseController
    {
        #region private variables

        private readonly GenericRepository<FARToBeVerified> _dbRepositoryFAR;
        private readonly GenericRepository<PlantAreaMaster> _dbRepositoryPlant;

        #endregion

        #region Constructor
        public ReportController()
        {
            _dbRepositoryFAR = new GenericRepository<FARToBeVerified>();
            _dbRepositoryPlant = new GenericRepository<PlantAreaMaster>();
        }
        #endregion

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult KendoReadPlanDD()
        {
            var list = _dbRepositoryPlant.GetEntities().Select(m=>new { Id = m.ID , Name = m.ID + " - " + m.BA + " - " + m.PLANT_NAME }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
    }
}