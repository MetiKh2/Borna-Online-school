using Borna.Application.Interfaces;
using Borna.Domain.Entities.Course;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharpCompress.Archives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Borna.Site.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;
        public CourseController(ICourseService courseService, IUserService userService, IPermissionService permissionService)
        {
            _permissionService = permissionService;
            _userService = userService;
            _courseService = courseService;
        }
        // GET: CourseController
        public ActionResult Index(int? groupeId = null, int pageId = 1, string filter = "", int take = 1)
        {
            ViewBag.Groupes = _courseService.GetGroupes();
            ViewBag.filter = filter;
            ViewBag.LastCourses = _courseService.GetLastCourses();
            if (!string.IsNullOrEmpty(filter) || groupeId != null)
            {
                ViewBag.NoPaging = true;
            }
            return View(_courseService.GetCoursesByFilter(groupeId, pageId, filter, take));
        }

        // GET: CourseController/Details/5
        [Route("ShowCourse/{id}")]
        public ActionResult ShowCourse(int id, int? episodeId)
        {
            var course = _courseService.GetCourseByID(id);
            ViewBag.Status = _courseService.GetCourseStatusTitleByID(course.StatusID);
            ViewBag.teacherAvatar = _courseService.GetTeacherAvatarByTeacherID(course.TeacherID);
            ViewBag.teacherName = _courseService.GetTeacherNameByTeacherID(course.TeacherID);
            ViewBag.language = _courseService.GetCourseLanguageTitleByID(course.LanguageID);
            ViewBag.lavel = _courseService.GetCourseLevelTitleByID(course.LevelID);
            ViewBag.parentGroupe = _courseService.GetCourseParentGroupeTitleByID(course.ParentGroupeID);
            ViewBag.subGroupe = _courseService.GetCourseSubGroupeTitleByID(course.SubGroupeID.Value);
            ViewBag.UsersCount = _courseService.GetUsersCourseCount(id);

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.UserBuy = _courseService.IsUserInCourse(_userService.GetUserIDByUserName(User.Identity.Name), id);
            }
            else
            {
                ViewBag.UserBuy = false;
            }

            if (episodeId != null)
            {

                if (User.Identity.IsAuthenticated)
                {
                    var episode = _courseService.GetCourseEpisodeByID(episodeId.Value);
                    if (course.CourseEpisodes.All(e => e.EpisodeID != episodeId))
                    {
                        return NotFound();
                    }
                    if (!course.CourseEpisodes.First(p => p.EpisodeID == episodeId).IsFree)
                    {
                        if (!_courseService.IsUserInCourse(_userService.GetUserIDByUserName(User.Identity.Name), id))
                        {
                            return NotFound();
                        }
                    }

                    var ep = course.CourseEpisodes.FirstOrDefault(p => p.EpisodeID == episodeId);
                    ViewBag.Episode = ep;
                    string filePath;
                    string checkFilePath;
                    if (ep.IsFree)
                    {
                        filePath = "/OnlineFreeEpisode/" + ep.EpisodeFile.Replace(".rar", ".mp4");
                        checkFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/OnlineFreeEpisode", ep.EpisodeFile.Replace(".rar", ".mp4"));
                    }
                    else
                    {
                        filePath = "/OnlineBuyEpisode/" + ep.EpisodeFile.Replace(".rar", ".mp4");
                        checkFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/OnlineBuyEpisode", ep.EpisodeFile.Replace(".rar", ".mp4"));
                    }
                    if (!System.IO.File.Exists(checkFilePath))
                    {
                        string targetPath ;
                        if (ep.IsFree)
                        {
                            targetPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/OnlineFreeEpisode");
                        }
                        else
                        {
                            targetPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/OnlineBuyEpisode");
                        }

                        string rarPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/Course/Episodes",ep.EpisodeFile);
                        var archive = ArchiveFactory.Open(rarPath);

                        var Entries = archive.Entries.OrderBy(x => x.Key.Length);

                        foreach (var item in Entries)
                        {
                            if (Path.GetExtension(item.Key) ==".mp4")
                            {
                                item.WriteTo(System.IO.File.Create(Path.Combine(targetPath, ep.EpisodeFile.Replace(".rar", ".mp4"))));
                            }
                        }
                    }
                    ViewBag.onlineFile = filePath;
                }
            }
            return View(course);
        }




        // GET: CourseController/Create
        [Route("DownloadEpisode/{id}")]
        [Authorize]
        public ActionResult DownloadEpisode(int id)
        {
            var episode = _courseService.GetCourseEpisodeByID(id);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/Episodes", episode.EpisodeFile);

            if (episode.IsFree)
            {
                byte[] file = System.IO.File.ReadAllBytes(filePath);
                return File(file, "application/force-download", episode.EpisodeFile);
            }

            if (_courseService.IsUserInCourse(_userService.GetUserIDByUserName(User.Identity.Name), episode.CourseID))
            {
                byte[] file = System.IO.File.ReadAllBytes(filePath);
                return File(file, "application/force-download", episode.EpisodeFile);
            }

            return BadRequest();
        }

        #region Comment

        public ActionResult AddComment(CourseComment comment)
        {
            comment.IsRemoved = false;
            comment.UserID = _userService.GetUserIDByUserName(User.Identity.Name);
            _courseService.AddComment(comment);
            return View("ShowComments", _courseService.GetCourseComments(comment.CourseID));
        }

        public IActionResult ShowComments(int id, int pageId = 1)
        {
            ViewBag.Checker = _permissionService.CheckPermission(User.Identity.Name, 12);
            return View(_courseService.GetCourseComments(id, pageId));
        }

        [Route("DeleteComment/{CommentID}")]

        public IActionResult DeleteComment(int CommentID, int courseId)
        {
            _courseService.DeleteComment(CommentID);

            return Redirect("/ShowCourse/" + courseId);
        }



        [HttpPost]
        [Route("file-upload")]
        public IActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();



            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/CommentsFile",
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);

            }



            var url = $"{"/CommentsFile/"}{fileName}";


            return Json(new { uploaded = true, url });
        }


        #endregion
        #region Vote
        public IActionResult ShowVotes(int id)
        {
            return View(_courseService.GetVotes(id));
        }

        public IActionResult AddVote(int courseId, bool vote)
        {
            CourseVote courseVote = new CourseVote
            {
                CourseID = courseId,
                UserID = _userService.GetUserIDByUserName(User.Identity.Name),
                Vote = vote
            };
            if (!User.Identity.IsAuthenticated)
            {
                return PartialView("ShowVotes", _courseService.GetVotes(courseId));
            }
            if (!_courseService.CourseIsFree(courseId))
            {
                if (!_courseService.IsUserInCourse(_userService.GetUserIDByUserName(User.Identity.Name), courseId))
                {
                    return PartialView("ShowVotes", _courseService.GetVotes(courseId));
                }
            }



            _courseService.AddVote(vote, _userService.GetUserIDByUserName(User.Identity.Name), courseId);
            return PartialView("ShowVotes", _courseService.GetVotes(courseId));
        }
        #endregion




    }
}
