using PVA.Web.Common;
using PVA.Web.Data;
using PVA.Web.Models;
using PVA.Web.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace PVA.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly GenericRepository<UserLoginDetail> _dbRepository;
        private IFormsAuthenticationService FormsService { get; set; }


        public LoginController()
        {
            _dbRepository = new GenericRepository<UserLoginDetail>();

        }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null)
            {
                FormsService = new FormsAuthenticationService();
            }

            base.Initialize(requestContext);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            if (!ModelState.IsValid) return View("Index", model);

            UserLoginDetail logedInUser = _dbRepository.GetEntities().FirstOrDefault(m => m.LoginID == model.UserName && m.Password == model.Password);

            if (logedInUser == null)
            {
                ModelState.AddModelError("UserName", "Invalid Login Credentials.");
                return View(model);
            }

            SessionHelper.UserId = logedInUser.ID;
            SessionHelper.WelcomeUser = logedInUser.Name;
            FormsService.SignIn(logedInUser.LoginID, true);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

    }
    #region Form Authentication
    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        public void SignIn(string userName, bool createPersistentCookie)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException(string.Empty, nameof(userName));
            }

            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }

    internal interface IFormsAuthenticationService
    {
        void SignIn(string userName, bool createPersistentCookie);

        void SignOut();
    }
    #endregion
}