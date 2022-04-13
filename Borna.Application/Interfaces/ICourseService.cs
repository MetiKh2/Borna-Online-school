using Borna.Domain.Entities.Course;
using Borna.Domain.Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borna.Application.Interfaces
{
   public interface ICourseService
    {
        #region Groupe
        void AddCourseGroupe(CourseGroupe courseGroupe);
        Tuple<List<CourseGroupe>, List<CourseGroupe>> GetCourseGroupes(string filter="");
        bool DeleteCourseGroupe(int groupeId);
        CourseGroupe GetCourseGroupeByID(int id);
        void EditCourseGroupe(CourseGroupe courseGroupe);
        List<CourseGroupe> GetGroupes();
        #endregion

        #region Course
        List<SelectListItem> GetParentGroupesForAddCourse();
        List<SelectListItem> GetSubGroupesForAddCourse(int parentId);
        List<SelectListItem> GetStatuesForAddCourse();
        List<SelectListItem> GetLevelsForAddCourse();
        List<SelectListItem> GetLanguagesForAddCourse();
        List<SelectListItem> GetTeachersForAddCourse();
        int AddCourse(Course course,IFormFile image);
        Tuple<List<Course>,int> GetCourses(int pageId = 1, string filtername = "");
        string GetCourseStatusTitleByID(int id);
        string GetCourseLevelTitleByID(int id);
        string GetCourseLanguageTitleByID(int id);
        string GetCourseParentGroupeTitleByID(int id);
        string GetCourseSubGroupeTitleByID(int id);
        bool DeleteCourse(int id);
        Course GetCourseByID(int id);
        void EditCourse(Course course,IFormFile image);
        //  string GetSubGroupesTitleByCourseID(int id);
        string GetTeacherAvatarByTeacherID(int id);
        string GetTeacherNameByTeacherID(int id);
        Tuple<List<Course>, int> GetCoursesByFilter(int? groupeId=null,int pageId=1,string filter="",int take=9);
        List<Course> GetLastCourses(int take=2);
        bool IsUserInCourse(int userId,int courseId);
        List<int> GetUserCoursesID(int userId);
        List<Course> GetPopularCourses();
        int GetUsersCourseCount(int courseId);
        bool CourseIsFree(int courseId);
        int CourseCount();
        int StudentCount();
        #endregion

        #region Episode
        bool IsExistCourseFile(string file);
        void AddEpisode(CourseEpisode episode,IFormFile file);
        List<CourseEpisode> GetCourseEpisodesByCourseID(int id);
        bool DeleteEpisode(int episodeId);
        CourseEpisode GetCourseEpisodeByID(int id);
        void EditEpisode(CourseEpisode episode, IFormFile file);
        #endregion

        #region Comment

        void AddComment(CourseComment comment);
        Tuple<List<CourseComment>, int> GetCourseComments(int courseId,int pageId=1);
        bool DeleteComment(int id);
        int GetCommentsCount(int courseId);
        #endregion

        #region Vote
        void AddVote(bool vote , int userId,int courseId);
        void EditVote(CourseVote vote);
        Tuple<int, int> GetVotes(int courseId);
        #endregion
    }
}
