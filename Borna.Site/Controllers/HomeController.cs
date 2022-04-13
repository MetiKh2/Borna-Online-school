using Borna.Application.Interfaces;
using Borna.Site.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Borna.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICourseService _courseService;
        //private readonly IUserService _userService;
        //private readonly IPermissionService _permissionService;
        private readonly ILogger<HomeController> _logger;
        public HomeController(ICourseService courseService, /*IUserService userService, IPermissionService permissionService,*/ ILogger<HomeController> logger)
        {
            //_permissionService = permissionService;
            //_userService = userService;
            _courseService = courseService; _logger = logger;
        }
      

     

        public IActionResult Index()
        {
            ViewBag.CourseCount = _courseService.CourseCount();
            ViewBag.StudenCount = _courseService.StudentCount();
            return View(_courseService.GetPopularCourses());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
