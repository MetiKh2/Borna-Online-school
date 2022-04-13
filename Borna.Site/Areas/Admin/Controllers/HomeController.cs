using Borna.Application.Security;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Borna.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        [PermissionChecker(1)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
