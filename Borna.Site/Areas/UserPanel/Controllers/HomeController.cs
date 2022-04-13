using Borna.Application.Interfaces;
using Borna.Common.Security;
using Borna.Domain.Entities.Course;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Borna.Site.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICourseService _courseService;
        public HomeController(IUserService userService,ICourseService courseService)
        {
            _courseService = courseService;
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region EditProfile
        [Route("EditProfile")]
        public IActionResult EditProfile()
        {
            ViewBag.FullName = _userService.GetFulNameByUserName(User.Identity.Name);
            return View();
        }

        [HttpPost]
        [Route("EditProfile")]

        public IActionResult EditProfile(string fullName, IFormFile image)
        {
            _userService.EditProfile(image, fullName, User.Identity.Name);
            return Redirect("/UserPanel");
        }
        #endregion


        #region EditPassword
        [Route("EditPassword")]
        public IActionResult EditPassword()
        {
            return View();
        }
        [Route("EditPassword")]
        [HttpPost]
        public IActionResult EditPassword(string currentPassword, string newPassword, string reNewPassword)
        {
            if (newPassword!=reNewPassword)
            {
                ViewBag.PasswordRePassword = true;
                return View();
            }
            var user = _userService.GetUserByUserName(User.Identity.Name);
            var hashCurrentPassword = MyPasswordHasher.EncodePasswordMd5(currentPassword);
            if (hashCurrentPassword==user.Password)
            {
                _userService.EditPassword(newPassword,User.Identity.Name);
                ViewBag.IsSeccess = true;
                return View();
            }
            ViewBag.WrongPassword = true;
            return View();
        }
        #endregion

        #region MyCourses
        [Route("MyCourses")]
        public IActionResult MyCourses()
        {
            var coursesId = _courseService.GetUserCoursesID(_userService.GetUserIDByUserName(User.Identity.Name));
            List<Course> Courses = new List<Course>();
            foreach (var id in coursesId)
            {
                Courses.Add(_courseService.GetCourseByID(id));
         

            }

            return View(Courses);
        }
        #endregion
    }
}
