using Borna.Application.Interfaces;
using Borna.Domain.Entities.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Borna.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        // GET: CourseController
        public ActionResult Index(int pageId=1,string filterName="")
        {
            ViewBag.currentPage = pageId;
            
            return View(_courseService.GetCourses(pageId,filterName));
        }

        // GET: CourseController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CourseController/Create
        public ActionResult AddCourse()
        {
            ViewBag.levels = new SelectList(_courseService.GetLevelsForAddCourse(),"Value","Text");
            ViewBag.languages = new SelectList(_courseService.GetLanguagesForAddCourse(),"Value","Text");
            ViewBag.statues = new SelectList(_courseService.GetStatuesForAddCourse(),"Value","Text");
            ViewBag.teachers = new SelectList(_courseService.GetTeachersForAddCourse(),"Value","Text");
            var parentGroupe = _courseService.GetParentGroupesForAddCourse();
            ViewBag.parentGroupes = new SelectList(parentGroupe,"Value","Text");
            ViewBag.subGroupes = new SelectList(_courseService.GetSubGroupesForAddCourse(int.Parse( parentGroupe.FirstOrDefault().Value)),"Value","Text");

            return View();
        }

        // POST: CourseController/Create
        [HttpPost]
        
        public ActionResult AddCourse(Course  course,IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.levels = new SelectList(_courseService.GetLevelsForAddCourse(), "Value", "Text");
                ViewBag.languages = new SelectList(_courseService.GetLanguagesForAddCourse(), "Value", "Text");
                ViewBag.statues = new SelectList(_courseService.GetStatuesForAddCourse(), "Value", "Text");
                ViewBag.teachers = new SelectList(_courseService.GetTeachersForAddCourse(), "Value", "Text");
                var parentGroupe = _courseService.GetParentGroupesForAddCourse();
                ViewBag.parentGroupes = new SelectList(parentGroupe, "Value", "Text");
                ViewBag.subGroupes = new SelectList(_courseService.GetSubGroupesForAddCourse(int.Parse(parentGroupe.FirstOrDefault().Value)), "Value", "Text");
                return View(course);
            }

            _courseService.AddCourse(course,image);
            return RedirectToAction("Index");
        }

        // GET: CourseController/Edit/5
        public ActionResult EditCourse(int id)
        {
            var course = _courseService.GetCourseByID(id);
            ViewBag.levels = new SelectList(_courseService.GetLevelsForAddCourse(), "Value", "Text",course.LevelID);
            ViewBag.languages = new SelectList(_courseService.GetLanguagesForAddCourse(), "Value", "Text",course.LanguageID);
            ViewBag.statues = new SelectList(_courseService.GetStatuesForAddCourse(), "Value", "Text",course.StatusID);
            ViewBag.teachers = new SelectList(_courseService.GetTeachersForAddCourse(), "Value", "Text",course.TeacherID);
            var parentGroupe = _courseService.GetParentGroupesForAddCourse();
            ViewBag.parentGroupes = new SelectList(parentGroupe, "Value", "Text",course.ParentGroupeID);
         //   ViewBag.subGroupes = new SelectList(_courseService.GetSubGroupesForAddCourse(int.Parse(parentGroupe.FirstOrDefault().Value)), "Value", "Text",course.SubGroupeID);

            List<SelectListItem> listItems = new List<SelectListItem> {
            new SelectListItem{
            Text="لطفا انتخاب کنید",Value=""
            }
            };
            listItems.AddRange(_courseService.GetSubGroupesForAddCourse(course.ParentGroupeID));
            string selectedSubGroup = null;
            if (course.SubGroupeID != null)
            {
                selectedSubGroup = course.SubGroupeID.ToString();
            }
            ViewBag.subGroupes = new SelectList(listItems, "Value", "Text", selectedSubGroup);

            return View(course);
        }

        // POST: CourseController/Edit/5
        [HttpPost]
       
        public ActionResult EditCourse(Course course, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.levels = new SelectList(_courseService.GetLevelsForAddCourse(), "Value", "Text", course.LevelID);
                ViewBag.languages = new SelectList(_courseService.GetLanguagesForAddCourse(), "Value", "Text", course.LanguageID);
                ViewBag.statues = new SelectList(_courseService.GetStatuesForAddCourse(), "Value", "Text", course.StatusID);
                ViewBag.teachers = new SelectList(_courseService.GetTeachersForAddCourse(), "Value", "Text", course.TeacherID);
                var parentGroupe = _courseService.GetParentGroupesForAddCourse();
                ViewBag.parentGroupes = new SelectList(parentGroupe, "Value", "Text", course.ParentGroupeID);
                //   ViewBag.subGroupes = new SelectList(_courseService.GetSubGroupesForAddCourse(int.Parse(parentGroupe.FirstOrDefault().Value)), "Value", "Text",course.SubGroupeID);

                List<SelectListItem> listItems = new List<SelectListItem> {
            new SelectListItem{
            Text="لطفا انتخاب کنید",Value=""
            }
            };
                listItems.AddRange(_courseService.GetSubGroupesForAddCourse(course.ParentGroupeID));
                string selectedSubGroup = null;
                if (course.SubGroupeID != null)
                {
                    selectedSubGroup = course.SubGroupeID.ToString();
                }
                ViewBag.subGroupes = new SelectList(listItems, "Value", "Text", selectedSubGroup);
                return View(course);
            }

            _courseService.EditCourse(course, image);
            return RedirectToAction("Index");


        }

        // GET: CourseController/Delete/5
        public ActionResult DeleteCourse(int CourseID)
        {

            return Json(_courseService.DeleteCourse(CourseID));
        }

        
       


        public IActionResult GetSubGroups(int id)
        {
            List<SelectListItem> list = new List<SelectListItem> { 
            new SelectListItem{ 
            Text="لطفا انتخاب کنید",
            Value=""
            },
            };
            list.AddRange(_courseService.GetSubGroupesForAddCourse(id));
            return Json(new SelectList(list,"Value","Text"));
        }
    }
}
