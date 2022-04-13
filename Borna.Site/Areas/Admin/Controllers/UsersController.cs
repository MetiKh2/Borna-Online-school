using Borna.Application.Interfaces;
using Borna.Application.Security;
using Borna.Domain.Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Borna.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;

        public UsersController(IUserService userService, IPermissionService permissionService)
        {
            _permissionService = permissionService;
            _userService = userService;
        }
        [PermissionChecker(2)]
        public IActionResult Index(int pageId = 1, string filtername = "", string filteremail = "")
        {
            ViewBag.currentPage = pageId;
            return View(_userService.GetUsersForAdmin(pageId, filtername, filteremail));
        }

        #region AddUser
        [PermissionChecker(3)]
        public IActionResult AddUser()
        {
            ViewBag.Roles = _permissionService.GetRoles();
            return View();
        }

        [HttpPost]
        [PermissionChecker(3)]
        public IActionResult AddUser(User user, IFormFile Image, List<int> selectedRoles)
        {
            user.UserAvatar = "Default.jpg";
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = _permissionService.GetRoles();
                return View(user);
            }
            if (_userService.UserNameExist(user.UserName))
            {
                ViewBag.Roles = _permissionService.GetRoles();
                ModelState.AddModelError("UserName", " نام کاربری قبلا مورد استفاده قرار گرفته است");
                return View(user);
            }
            if (_userService.EmailExist(user.Email))
            {
                ViewBag.Roles = _permissionService.GetRoles();
                ModelState.AddModelError("Email", " ایمیل  قبلا مورد استفاده قرار گرفته است");
                return View(user);
            }

            int userId = _userService.AddUserForAdmin(user, Image);
            _permissionService.AddUserRoles(selectedRoles, userId);
            return RedirectToAction("Index");
        }
        #endregion

        #region DeleteUser
        [HttpPost]
        [PermissionChecker(5)]
        public IActionResult DeleteUser(int UserID)
        {
            return Json(_userService.DeleteUser(UserID));
        }
        #endregion
        #region EditUser
        [PermissionChecker(6)]
        public IActionResult EditUser(int id)
        {
            ViewBag.Roles = _permissionService.GetRoles();
            ViewBag.UserRoles = _permissionService.GetRolesByUserId(id);
            var user = _userService.GetUserByID(id);
            return View(user);
        }

        [HttpPost]
        [PermissionChecker(6)]
        public IActionResult EditUser(User user, IFormFile Image, string newPassword,List<int> selectedRoles)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = _permissionService.GetRoles(); 
                ViewBag.UserRoles = _permissionService.GetRolesByUserId(user.UserID);
                return View(user);
            }

            _userService.EditUser(user, Image, newPassword);
            _permissionService.EditUserRoles(selectedRoles,user.UserID);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
