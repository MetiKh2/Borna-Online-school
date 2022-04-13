using Borna.Application.Interfaces;
using Borna.Application.Security;
using Borna.Domain.Entities.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Borna.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly IPermissionService _permissionService;

        public RolesController(IPermissionService permissionService)
        {

            _permissionService = permissionService;
        }
        [PermissionChecker(7)]
        public IActionResult Index(string filterRole = "")
        {
            var roles = _permissionService.GetRoles(filterRole);
            return View(roles);
        }

        #region AddRole
        [PermissionChecker(8)]
        public IActionResult AddRole()
        {
            ViewBag.Permissions = _permissionService.GetPermissions();
            return View();
        }
        [PermissionChecker(8)]
        [HttpPost]
        public IActionResult AddRole(Role role, List<int> selectedPermissions)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Permissions = _permissionService.GetPermissions();
                return View(role);
            }
            int roleId = _permissionService.AddRole(role);
            _permissionService.AddRolePermissions(roleId, selectedPermissions);
            return RedirectToAction("Index");
        }
        #endregion

        #region DeleteRole
        [PermissionChecker(10)]
        public IActionResult DeleteRole(int RoleID)
        {
            return Json(_permissionService.DeleteRole(RoleID));
        }
        #endregion


        #region EditRole 
        [PermissionChecker(9)]
        public IActionResult EditRole(int id)
        {
            ViewBag.RolePermissions = _permissionService.GetRolePermissionsByID(id);
            ViewBag.Permissions = _permissionService.GetPermissions();
            var role = _permissionService.GetRoleByID(id);
            return View(role);
        }
        [HttpPost]
        [PermissionChecker(9)]
        public IActionResult EditRole(Role role,List<int> selectedPermissions)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Permissions = _permissionService.GetPermissions();
                ViewBag.RolePermissions = _permissionService.GetRolePermissionsByID(role.RoleID);
                return View();
            }
          int roleId=  _permissionService.EditRole(role);
            _permissionService.EditRolePermissions(roleId,selectedPermissions);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
