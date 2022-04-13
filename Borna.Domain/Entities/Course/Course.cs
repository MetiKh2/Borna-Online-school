using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borna.Domain.Entities.Course
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }

        [Required]
        public int LevelID { get; set; }

        [Required]
        public int LanguageID { get; set; }

        [Required]
        public int ParentGroupeID { get; set; }

        public int? SubGroupeID { get; set; }

        [Required]
        public int StatusID { get; set; }

        [Required]
        public int TeacherID { get; set; }


        [Display(Name = "عنوان دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string CourseTitle { get; set; }

        [Display(Name = "شرح دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      
        public string CourseDescription { get; set; }

        [Display(Name = "قیمت دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Price { get; set; }

        [Display(Name = "عکس دوره")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string CourseImage { get; set; }

        [Display(Name = "حذف شده؟")]
        public bool IsRemoved { get; set; } = false;


        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }

        #region Rel
        [ForeignKey("ParentGroupeID")]
        public CourseGroupe ParentCourseGroupe { get; set; }
        [ForeignKey("SubGroupeID")]
        public CourseGroupe SubCourseGroupe { get; set; }

        public CourseLevel CourseLevel { get; set; }

        public CourseLanguage  CourseLanguage { get; set; }
      

        public CourseStatus CourseStatus { get; set; }

        [ForeignKey("TeacherID")]
        public User.User Teacher { get; set; }

        public List<CourseEpisode> CourseEpisodes { get; set; }
        public List<UserCourse> UserCourses { get; set; }
        public List<CourseComment> CourseComments { get; set; }
        public List<CourseVote> CourseVotes { get; set; }
        #endregion

    }
}
