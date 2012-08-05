using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Oktogo.UserManagement.Web.UserManagementService;

namespace Oktogo.UserManagement.Web.Controllers
{
    public class UserController : Controller
    {
        private IUserManagementService UserManagementService { get; set; }

        public UserController(IUserManagementService userManagementService)
        {
            UserManagementService = userManagementService;
        }

        public ViewResult Index()
        {
            return View(UserManagementService.GetUsers());
        }

        public ActionResult Add()
        {
            return View("Edit", new User());
        }
        
        public ActionResult Edit(int id)
        {
            var user = UserManagementService.GetUser(id);
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                UserManagementService.SaveUser(user);
                TempData["Success"] = string.Format("{0} {1} has been successfully saved", user.FirstName, user.LastName);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (UserManagementService.DeleteUser(id))
            {
                TempData["Success"] = "User has been succesfully deleted";
            }
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Test app.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Marina Kolesnik.";
            return View();
        }
    }
}
