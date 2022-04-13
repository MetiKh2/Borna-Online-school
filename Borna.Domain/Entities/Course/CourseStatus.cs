using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borna.Domain.Entities.Course
{
   public class CourseStatus
    {
        [Key]
        public int StatusID { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string StatusTitle { get; set; }


        #region Rel
        public List<Course> Courses { get; set; }
        #endregion
    }
}
