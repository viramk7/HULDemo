using OfficeOpenXml;
using PVA.Web.Data;
using PVA.Web.Models;
using PVA.Web.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PVA.Web.Controllers
{
    public class ImportDataController : BaseController
    {
        #region private variables

        private readonly GenericRepository<FARToBeVerified> _dbRepositoryFAR;
        private readonly GenericRepository<PlantAreaMaster> _dbRepositoryPlant;

        #endregion

        #region Constructor
        public ImportDataController()
        {
            _dbRepositoryFAR = new GenericRepository<FARToBeVerified>();
            _dbRepositoryPlant = new GenericRepository<PlantAreaMaster>();
        }
        #endregion

        #region Methods
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ImportItems(IEnumerable<HttpPostedFileBase> fileImport)
        {
            foreach (var item in fileImport)
            {
                if (item != null)
                {
                    var package = new ExcelPackage(item.InputStream);
                    ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                    workSheet.TrimLastEmptyRows();

                    IEnumerable<ExcelImportModel> listFromExcel = GetDataFromExcelStockCheck(workSheet, true);

                    try
                    {
                        foreach(var obj in listFromExcel)
                        {
                            if (!string.IsNullOrEmpty(obj.BA))
                            {
                                PlantAreaMaster plantAreaMaster = _dbRepositoryPlant.GetEntities().Where(m => m.BA == obj.BA).FirstOrDefault();
                                if(plantAreaMaster != null)
                                {
                                    string plantId = plantAreaMaster.ID;
                                    FARToBeVerified farObj = new FARToBeVerified();
                                    farObj.ID = Guid.NewGuid().ToString();
                                    farObj.PlantID = plantId;
                                    farObj.AssetIDSubNo = obj.AssetId;
                                    farObj.Process = obj.Process;
                                    farObj.Product = obj.Product;
                                    farObj.Pack = obj.Pack;
                                    farObj.EquipmentDetails = obj.EquipmentDetails;
                                    farObj.AssetDescription = obj.AssetDescription;
                                    farObj.InstallationStatus = obj.InstallationStatus;
                                    farObj.Status = "Pending";
                                    farObj.CreatedBy = "2dfadc7a-2026-4976-8161-900cf3eafae5";
                                    farObj.CreatedDate = System.DateTime.Now;
                                    farObj.ModifiedBy= "2dfadc7a-2026-4976-8161-900cf3eafae5";
                                    farObj.ModifiedDate = System.DateTime.Now;
                                    farObj.EnteredOnMachineID = "DESKTOP-RVA968U";
                                    farObj.ExeVersionNo = "v 1.0.1    22-Oct-2019";
                                    farObj.APSyncModifiedBy = "G";
                                    farObj.SysCreatedDateTime = System.DateTime.Now;
                                    farObj.SysModifiedDateTime = System.DateTime.Now;
                                    _dbRepositoryFAR.Insert(farObj);
                                }
                            }
                        }
                    }

                    catch (DbEntityValidationException dbEx)
                    {
                        string messages = String.Empty;
                        foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                        {
                            foreach (DbValidationError error in entityErr.ValidationErrors)
                            {
                                //Console.WriteLine("Error Property Name {0} : Error Message: {1}",
                                //                    error.PropertyName, error.ErrorMessage);
                                messages = messages + "<br/>" + string.Join("<br/>", error.ErrorMessage);

                            }
                        }

                    }
                }

                else
                {
                    return RedirectToAction("Index", "ImportData");
                }
            }

            return RedirectToAction("Index", "ImportData");
        }


        public IEnumerable<ExcelImportModel> GetDataFromExcelStockCheck(ExcelWorksheet workSheet, bool firstRowHeader)
        {
            IList<ExcelImportModel> tblItemModel = new List<ExcelImportModel>();

            if (workSheet != null)
            {
                Dictionary<string, int> header = new Dictionary<string, int>();

                for (int rowIndex = workSheet.Dimension.Start.Row; rowIndex <= workSheet.Dimension.End.Row; rowIndex++)
                {

                    //Assume the first row is the header. Then use the column match ups by name to determine the index.
                    //This will allow you to have the order of the columns change without any affect.

                    if (rowIndex == 1 && firstRowHeader)
                    {
                        header = ExcelHelper.GetExcelHeader(workSheet, rowIndex);
                    }
                    else
                    {
                        tblItemModel.Add(new ExcelImportModel
                        {
                            BA = ExcelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "Business Area"),
                            AssetId = ExcelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "Asset ID & Sub no"),
                            Process = ExcelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "Process"),
                            Product = ExcelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "Product"),
                            Pack = ExcelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "Pack"),
                            EquipmentDetails = ExcelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "Equipment Details"),
                            AssetDescription = ExcelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "Asset description"),
                            InstallationStatus = ExcelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "Installation Status")
                        });

                    }
                }
            }

            return tblItemModel;
        }
        #endregion
    }
}