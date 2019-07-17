using OfficeOpenXml;
using PVA.Web.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PVA.Web.Controllers
{
    public class ImportDataController : Controller
    {
        // GET: ImportData
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public ActionResult ImportItems(FARToBeVerified Excelmodel, IEnumerable<HttpPostedFileBase> ItemList)
        {


            foreach (var item in ItemList)
            {
                if (item != null)
                {
                    var package = new ExcelPackage(item.InputStream);
                    ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                    workSheet.TrimLastEmptyRows();

                    IEnumerable<FARToBeVerified> listFromExcel = GetDataFromExcelStockCheck(workSheet, true);

                    try
                    {
                        //foreach (var modelItem in listFromExcel)
                        //{
                        //    foreach (var brandId in Excelmodel.BrandIds)
                        //    {
                        //        bool result = CustomRepository.CheckItemExits(brandId, Excelmodel.CategoryId, modelItem.ITEMDESCRIPTION);
                        //        if (result == false)
                        //        {
                                  
                        //        }


                        //    }

                        //}

                        
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
                    return View("ImportItemFromExcel", Excelmodel);
                }
            }


            return RedirectToAction("Index", "Item");
        }



        public IEnumerable<FARToBeVerified> GetDataFromExcelStockCheck(ExcelWorksheet workSheet, bool firstRowHeader)
        {
            IList<FARToBeVerified> tblItemModel = new List<FARToBeVerified>();

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
                        //tblItemModel.Add(new ExcelItemDetails
                        //{
                        //    ITEMDESCRIPTION = ExcelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "ITEM DESCRIPTION"),
                        //    HSNCODE = ExcelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "HSN CODE"),
                        //    UNIT = ExcelHelper.ParseWorksheetValue(workSheet, header, rowIndex, "UNIT")
                        //});

                    }
                }
            }

            return tblItemModel;
        }
    }
}