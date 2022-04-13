using Borna.Application.Interfaces;
using Borna.Common.Convertors;
using Borna.Common.Generators;
using Borna.Common.Security;
using Borna.Domain.Entities.Course;
using Borna.Domain.Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Borna.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly IDataBaseContext _context;
        public CourseService(IDataBaseContext context)
        {
            _context = context;
        }

        public void AddComment(CourseComment comment)
        {
            _context.CourseComments.Add(comment);
            _context.SaveChanges();
        }

        public int AddCourse(Course course, IFormFile image)
        {

            course.CourseImage = "Default.jpg";
            if (image != null&&image.IsImage())
            {

                course.CourseImage = GeneratCode.GenerateUniqCode() + Path.GetExtension(image.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/CourseImage", course.CourseImage);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }

                var imageThumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/ThumbCourseImage", course.CourseImage);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }

                ImageResizer resizer = new ImageResizer();

                resizer.Image_resize(imagePath, imageThumbPath, 166);
            }
            _context.Courses.Add(course);
            _context.SaveChanges();
            return course.CourseID;
          
        }

        public void AddCourseGroupe(CourseGroupe courseGroupe)
        {
            _context.CourseGroupes.Add(courseGroupe);
            _context.SaveChanges();

        }

        public void AddEpisode(CourseEpisode episode, IFormFile file)
        {
            if (file!=null)
            {
                episode.EpisodeFile = file.FileName;
                var imagePath=   Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/Episodes", episode.EpisodeFile);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            _context.CourseEpisodes.Add(episode);
            _context.SaveChanges();
        }

        public void AddVote(bool vote, int userId, int courseId)
        {
            var courseVote = _context.CourseVotes.FirstOrDefault(p => p.UserID == userId && p.CourseID == courseId);
            if (courseVote==null)
            {
                CourseVote newCourseVote = new CourseVote { 
                CourseID=courseId,UserID=userId,Vote=vote
                };
                _context.CourseVotes.Add(newCourseVote);
                _context.SaveChanges();
            }
            else
            {
                courseVote.Vote = vote;
                _context.CourseVotes.Update(courseVote);
                _context.SaveChanges();
            }
        }

        public int CourseCount()
        {
            return _context.Courses.Count();

        }

        public bool CourseIsFree(int courseId)
        {
            var course = _context.Courses.Where(p => p.CourseID == courseId).FirstOrDefault();

            return course.Price == 0;
        }

        public bool DeleteComment(int id)
        {
            try
            {
                var comment = _context.CourseComments.Find(id);
                comment.IsRemoved = true;
                _context.CourseComments.Update(comment);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool DeleteCourse(int id)
        {
            try
            {
                var course = _context.Courses.Find(id);
                course.IsRemoved = true;
                _context.Courses.Update(course);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool DeleteCourseGroupe(int groupeId)
        {
            try
            {
                var courseGroupe = _context.CourseGroupes.Where(p => p.GroupeID == groupeId).FirstOrDefault();

                courseGroupe.IsRemoved = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool DeleteEpisode(int episodeId)
        {
            try
            {

                var episode = _context.CourseEpisodes.Find(episodeId);
                episode.IsRemoved = true;
                _context.CourseEpisodes.Update(episode);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public void EditCourse(Course course, IFormFile image)
        {
            if (image!=null&&image.IsImage())
            {
                if (course.CourseImage!= "Default")
                {
                    var pathDelete = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/CourseImage", course.CourseImage);
                    if (File.Exists(pathDelete))
                    {
                        File.Delete(pathDelete);
                    }
                    var thumbPathDelete = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/ThumbCourseImage", course.CourseImage);
                    if (File.Exists(thumbPathDelete))
                    {
                        File.Delete(thumbPathDelete);
                    }
                }

                course.CourseImage = GeneratCode.GenerateUniqCode() + Path.GetExtension(image.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/CourseImage", course.CourseImage);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }

                var imageThmbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/ThumbCourseImage", course.CourseImage);
                ImageResizer resizer = new ImageResizer();
                resizer.Image_resize(imagePath,imageThmbPath,166);

            }

            _context.Courses.Update(course);
            _context.SaveChanges();

        }

        public void EditCourseGroupe(CourseGroupe courseGroupe)
        {
            _context.CourseGroupes.Update(courseGroupe);
            _context.SaveChanges();
        }

        public void EditEpisode(CourseEpisode episode, IFormFile file)
        {
            if (file!=null)
            {
                string imagePathDelete = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/Episodes",episode.EpisodeFile);
                if (File.Exists(imagePathDelete))
                {
                    File.Delete(imagePathDelete);
                }
                
                episode.EpisodeFile = file.FileName;

                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/Episodes", episode.EpisodeFile);
                using (var stream=new FileStream(imagePath,FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            _context.CourseEpisodes.Update(episode);
            _context.SaveChanges();
        }

        public void EditVote(CourseVote vote)
        {
            _context.CourseVotes.Update(vote);
            _context.SaveChanges();
        }

        public int GetCommentsCount(int courseId)
        {
            return _context.CourseComments.Where(p => p.CourseID == courseId).Count();
        }

        public Course GetCourseByID(int id)
        {
            return _context.Courses.Include(p => p.Teacher).Include(p => p.CourseStatus).Include(p => p.CourseLevel).Include(p => p.CourseLanguage)
                .Include(p => p.SubCourseGroupe).Include(p => p.ParentCourseGroupe).Include(p => p.CourseEpisodes).Include(p=>p.CourseComments).FirstOrDefault(p=>p.CourseID==id);
        }

        public Tuple<List<CourseComment>, int> GetCourseComments(int courseId, int pageId = 1)
        {
            int take = 5;
            int skip = (pageId-1) * take;
            var comments = _context.CourseComments.Include(p=>p.User).Where(p => p.CourseID == courseId);
            var pageCount = comments.Count() / take;
            return Tuple.Create(comments.OrderByDescending(p=>p.CommentID).Skip(skip).Take(take).ToList(),pageCount);
        }

        public CourseEpisode GetCourseEpisodeByID(int id)
        {
            return _context.CourseEpisodes.Where(p => p.EpisodeID == id).FirstOrDefault();
        }

        public List<CourseEpisode> GetCourseEpisodesByCourseID(int id)
        {
            return _context.CourseEpisodes.Where(p => p.CourseID == id).ToList();
        }

        public CourseGroupe GetCourseGroupeByID(int id)
        {
            return _context.CourseGroupes.Find(id);
        }

        public Tuple<List<CourseGroupe>, List<CourseGroupe>> GetCourseGroupes(string filter = "")
        {
            IQueryable<CourseGroupe> parentCourseGroupes = _context.CourseGroupes.Where(p=>p.ParentID==null);
            if (!string.IsNullOrEmpty(filter))
            {
                parentCourseGroupes = parentCourseGroupes.Where(p => p.GroupeTitle.Contains(filter));
                
            }
            return Tuple.Create(parentCourseGroupes.ToList(), _context.CourseGroupes.Where(p => p.ParentID != null).ToList());
        }

        public string GetCourseLanguageTitleByID(int id)
        {
            return _context.CourseLanguages.Where(p => p.LanguageID == id).Select(p => p.LanguageTitle).FirstOrDefault();
        }

        public string GetCourseLevelTitleByID(int id)
        {
            return _context.CourseLevels.Where(p => p.LevelID == id).Select(p => p.LevelTitle).FirstOrDefault();
        }

        public string GetCourseParentGroupeTitleByID(int id)
        {
            return _context.CourseGroupes.Where(p => p.GroupeID == id).Select(p => p.GroupeTitle).FirstOrDefault();
        }

        public Tuple<List<Course>, int> GetCourses(int pageId=1,string filtername="")
        {
            int take = 5;
            int skip = (pageId-1) * take;
            IQueryable<Course> courses = _context.Courses.Include(p => p.CourseLevel).Include(p => p.CourseStatus).Include(p => p.CourseLanguage).Include(p => p.Teacher)
                  .Include(p => p.ParentCourseGroupe).Include(p => p.SubCourseGroupe);

            if (!string.IsNullOrEmpty(filtername))
            {
                courses = courses.Where(p => p.CourseTitle.Contains(filtername));
                take = 10000;
            }

            return Tuple.Create(courses.Skip(skip).Take(take).ToList(), courses.ToList().Count() / take);
               

        }

        public Tuple<List<Course>, int> GetCoursesByFilter(int? groupeId=null, int pageId = 1, string filter = "",int take=9)
        {
            IQueryable<Course> Courses = _context.Courses.Include(p=>p.ParentCourseGroupe).Include(p=>p.CourseComments);
            if (!string.IsNullOrEmpty(filter))
            {
                Courses = Courses.Where(p => p.CourseTitle.Contains(filter));
                take = 10000;
            }
            if (groupeId!=null)
            {
                Courses = Courses.Where(p => p.ParentGroupeID==groupeId||p.SubGroupeID==groupeId);
                take = 10000;
            }
            int skip = (pageId-1) * take;
            int totalPage = Courses.ToList().Count() / take;
            return Tuple.Create(Courses.OrderByDescending(p=>p.CourseID).Skip(skip).Take(take).ToList(),totalPage);

        }

        public string GetCourseStatusTitleByID(int id)
        {
            return _context.CourseStatuses.Where(p => p.StatusID == id).Select(p => p.StatusTitle).FirstOrDefault();

        }

        public string GetCourseSubGroupeTitleByID(int id)
        {
            return _context.CourseGroupes.Where(p => p.GroupeID == id).Select(p => p.GroupeTitle).FirstOrDefault();
        }

        public List<CourseGroupe> GetGroupes()
        {
            return _context.CourseGroupes.ToList();
        }

        public List<SelectListItem> GetLanguagesForAddCourse()
        {
            return _context.CourseLanguages.Select(p => new SelectListItem
            {
                Text = p.LanguageTitle,
                Value = p.LanguageID.ToString()
            }).ToList();
        }

        public List<Course> GetLastCourses(int take = 2)
        {
            return _context.Courses.Include(p=>p.CourseComments).OrderByDescending(p => p.CourseID).Take(take).ToList();
        }

        public List<SelectListItem> GetLevelsForAddCourse()
        {
            return _context.CourseLevels.Select(p => new SelectListItem
            {
                Text = p.LevelTitle,
                Value = p.LevelID.ToString()
            }).ToList();
        }

        public List<SelectListItem> GetParentGroupesForAddCourse()
        {
            return _context.CourseGroupes.Where(p => p.ParentID == null).ToList().Select(p => new SelectListItem
            {
                Text = p.GroupeTitle,
                Value = p.GroupeID.ToString()
            }).ToList();
        }

        public List<Course> GetPopularCourses()
        {
            List<int> coursesId = _context.CourseVotes.Where(p => p.Vote == true).OrderByDescending(p => p.CourseID).Select(p=>p.CourseID).Take(6).ToList();
            List<Course> courses = new List<Course>();

            foreach (var item in coursesId)
            {
                courses.Add(GetCourseByID(item));
            }
            return courses;
        }

        public List<SelectListItem> GetStatuesForAddCourse()
        {
            return _context.CourseStatuses.Select(p => new SelectListItem
            {
                Text = p.StatusTitle,
                Value = p.StatusID.ToString()
            }).ToList();
        }

        public List<SelectListItem> GetSubGroupesForAddCourse(int parentId)
        {
            return _context.CourseGroupes.Where(p => p.ParentID ==parentId).Select(p => new SelectListItem
            {
                Text = p.GroupeTitle,
                Value = p.GroupeID.ToString()
            }).ToList();
        }

        public string GetTeacherAvatarByTeacherID(int id)
        {
            return _context.Users.Where(p => p.UserID == id).Select(p => p.UserAvatar).FirstOrDefault();
        }

        public string GetTeacherNameByTeacherID(int id)
        {
            return _context.Users.Where(p => p.UserID == id).Select(p => p.UserName).FirstOrDefault();

        }

        //public string GetSubGroupesTitleByCourseID(int id)
        //{
        //   return _context.Courses.Include(p=>p.SubCourseGroupe).Where(p=>p.CourseID==id).Select(p=>p.sub)
        //}

        public List<SelectListItem> GetTeachersForAddCourse()
        {
            return _context.UserInRoles.Include(p=>p.User).Where(p=>p.RoleID==2).Select(p => new SelectListItem
            {
                Text = p.User.UserName,
                Value = p.UserID.ToString()
            }).ToList();
        }

        public List<int> GetUserCoursesID(int userId)
        {
            return _context.UserCourses.Include(p => p.Course).Where(p => p.UserID == userId).Select(p => p.CourseID).ToList();
        }

        public int GetUsersCourseCount(int courseId)
        {
            return _context.UserCourses.Where(p => p.CourseID == courseId).Count();
        }

        public Tuple<int, int> GetVotes(int courseId)
        {
            var votes = _context.CourseVotes.Where(p => p.CourseID == courseId).ToList();
            return Tuple.Create(votes.Where(p=>p.Vote==true).Count(), votes.Where(p => p.Vote == false).Count());
        }

        public bool IsExistCourseFile(string file)
        {
            return _context.CourseEpisodes.Any(p=>p.EpisodeFile==file);
        }

        public bool IsUserInCourse(int userId, int courseId)
        {
            return _context.UserCourses.Any(p=>p.UserID==userId&&p.CourseID==courseId);
        }

        public int StudentCount()
        {
            return _context.UserCourses.Count();
        }
    }
}
