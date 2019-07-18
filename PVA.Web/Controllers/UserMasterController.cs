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
    public class UserMasterController : BaseController
    {
        #region private variables

        private readonly GenericRepository<UserLoginDetail> _dbRepository;

        #endregion

        #region Constructor
        public UserMasterController()
        {
            _dbRepository = new GenericRepository<UserLoginDetail>();
        }
        #endregion

        #region Methods
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult KendoRead([DataSourceRequest] DataSourceRequest request)
        {
            if (!request.Sorts.Any())
            {
                request.Sorts.Add(new SortDescriptor("LoginID", ListSortDirection.Ascending));
            }

            return Json(_dbRepository.GetEntities().ToDataSourceResult(request));
        }

        public ActionResult Create()
        {
            return View(new UserLoginDetail { IsActive = true });
        }

        public ActionResult Edit(string id)
        {
            return View("Create", _dbRepository.SelectById(id));
        }

        public ActionResult SaveModelData(UserLoginDetail model, string create = null)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", model);
            }

            string message = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(model.ID))
                {
                    model.ModifiedBy = "1";
                    model.ModifiedDate = DateTime.Now;
                    message = _dbRepository.Update(model);
                }
                else
                {
                    model.ID = Guid.NewGuid().ToString();
                    model.CreatedBy = "1";
                    model.CreatedDate = DateTime.Now;
                    message = _dbRepository.Insert(model);
                }
                
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            if (!string.IsNullOrEmpty(model.ID))
            {
                if (create == "Save & Continue")
                {
                    return RedirectToAction("Edit", new { id = model.ID });
                }
                else if (create == "Save & New")
                {
                    return RedirectToAction("Create");
                }
            }
            //if (model.CustomerId > 0)
            //{
            //    TempData[Enums.NotifyType.Success.GetDescription()] = "Customer Updated Successfully.";
            //}
            //else
            //{
            //    TempData[Enums.NotifyType.Success.GetDescription()] = "Customer Created Successfully.";
            //}
            return RedirectToAction("Index");
        }

        public string KendoDestroy(string id)
        {
            string deleteMessage = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    deleteMessage = _dbRepository.Delete(id);
                }
                else
                {
                    deleteMessage = "Something went wrong.";
                }
            }
            catch (Exception ex)
            {
                deleteMessage = ex.Message;
            }

            return deleteMessage;
        }

        public string ChangeStatus(string id)
        {
            UserLoginDetail user = _dbRepository.SelectById(id);
            user.IsActive = !user.IsActive;
            return _dbRepository.Update(user);
        }
        #endregion
    }
}