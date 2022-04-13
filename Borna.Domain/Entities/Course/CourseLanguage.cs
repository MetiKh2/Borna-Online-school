using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borna.Domain.Entities.Course
{
   public class CourseLanguage
    {
        [Key]
        public int LanguageID { get; set; }

        [Display(Name = "زبان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string LanguageTitle { get; set; }


        #region Rel
        public List<Course> Courses { get; set; }
        #endregion
    }
}
