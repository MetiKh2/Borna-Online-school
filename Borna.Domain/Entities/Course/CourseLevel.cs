using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borna.Domain.Entities.Course
{
    public class CourseLevel
    {
        [Key]
        public int LevelID { get; set; }

        [Display(Name = "سطح")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string LevelTitle { get; set; }


        #region Rel
        public List<Course> Courses { get; set; }
        #endregion
    }
}
