using Borna.Domain.Entities.Course;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borna.Domain.Entities.User
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string FullName { get; set; }


        [Display(Name = "نام کاربری  ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Email { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Password { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegisterDate { get; set; } = DateTime.Now;

        [Display(Name = "وضعیت؟")]
        public bool IsActive { get; set; }

        [Display(Name = "کد فعالسازی")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string ActivationCode { get; set; }

        [Display(Name = "حذف شده؟")]
        public bool IsRemoved { get; set; } = false;

        [Display(Name = "تاریخ حذف")]
        public DateTime? RemovedDate { get; set; }
        
        [Display(Name = "اواتار ")]
   
        public string UserAvatar { get; set; }

        #region Rel
        public List<UserInRole> UserInRoles { get; set; }
        public List<UserCourse> UserCourses { get; set; }

        public List<CourseComment> CourseComments { get; set; }

        public List<CourseVote> CourseVotes { get; set; }

        #endregion
    }
}
