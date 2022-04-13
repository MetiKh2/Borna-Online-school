using Borna.Application.Interfaces;
using Borna.Domain.Entities.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Borna.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EpisodeController : Controller
    {
        private readonly ICourseService _courseService;
        public EpisodeController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        // GET: EpisodeController
        public ActionResult Index(int id)
        {
            ViewBag.CourseID = id;
            return View(_courseService.GetCourseEpisodesByCourseID(id));
        }

        // GET: EpisodeController/Details/5
        public ActionResult Details(int id)
        {
            
            return View();
        }

        // GET: EpisodeController/Create
        public ActionResult AddEpisode(int id)
        {
            return View(new CourseEpisode { CourseID = id });
        }

        // POST: EpisodeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEpisode(CourseEpisode episode,IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return View(episode);
            }

            if (file != null)
            {
                if (_courseService.IsExistCourseFile(file.FileName))
                {
                    ViewBag.ExistTitle = true;
                    return View(episode);
                }
            }
            _courseService.AddEpisode(episode,file);
            return Redirect("/Admin/episode/index/"+episode.CourseID);
        }

        // GET: EpisodeController/Edit/5
        public ActionResult Edit(int id)
        {
           
            return View(_courseService.GetCourseEpisodeByID(id));
        }

        // POST: EpisodeController/Edit/5
        [HttpPost]
      
        public ActionResult Edit(CourseEpisode episode, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return View(episode);
            }
            if (file!=null)
            {
                if (_courseService.IsExistCourseFile(file.FileName))
                {
                    ViewBag.ExistTitle = true;
                    return View(episode);
                }
            }
           

            _courseService.EditEpisode(episode,file);
            return Redirect("/Admin/episode/index/" + episode.CourseID);
        }

        // GET: EpisodeController/Delete/5

        //POST: EpisodeController/Delete/5
        [HttpPost]
        public IActionResult DeleteEpisode(int EpisodeID)
        {
            return Json(_courseService.DeleteEpisode(EpisodeID));
        }
    }
}
