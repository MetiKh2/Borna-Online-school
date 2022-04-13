using Borna.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Borna.Site.ViewComponents
{
    public class CourseGroupesComponent:ViewComponent
    {
        private readonly ICourseService _courseService;
        public CourseGroupesComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public IViewComponentResult Invoke()
        {
            return View("CourseGroupes",_courseService.GetGroupes());
        }
    }
}
