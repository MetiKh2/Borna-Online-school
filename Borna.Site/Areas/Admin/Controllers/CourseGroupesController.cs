using Borna.Application.Interfaces;
using Borna.Domain.Entities.Course;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Borna.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseGroupesController : Controller
    {
        private readonly ICourseService _courseService;
        public CourseGroupesController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public IActionResult Index(string filter="")
        {
            return View(_courseService.GetCourseGroupes(filter));
        }

        #region AddGroupe
        public IActionResult AddCourseGroupe(int? id)
        {
            return View(new CourseGroupe {ParentID=id });
        }
        [HttpPost]
        public IActionResult AddCourseGroupe(CourseGroupe courseGroupe)
        {
            if (!ModelState.IsValid)
            {
                return View(courseGroupe);
            }
            _courseService.AddCourseGroupe(courseGroupe);
            return RedirectToAction("Index");
        }
        #endregion

        #region DeleteCourseGroupe
        [HttpPost]
        public IActionResult DeleteCourseGroupe(int GroupeID)
        {
            return Json(_courseService.DeleteCourseGroupe(GroupeID));
        }
        #endregion

        #region EditCourseGroupe
        public IActionResult EditCourseGroupe(int id)
        {

            return View(_courseService.GetCourseGroupeByID(id));
        }
        [HttpPost]
        public IActionResult EditCourseGroupe(CourseGroupe courseGroupe)
        {
            if (!ModelState.IsValid)
            {
                return View(courseGroupe);
            }

            _courseService.EditCourseGroupe(courseGroupe);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
