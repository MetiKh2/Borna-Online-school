using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borna.Domain.Entities.Course
{
    public class CourseGroupe
    {
        [Key]
        public int GroupeID { get; set; }

        [Display(Name = "نام گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string GroupeTitle { get; set; }


        [Display(Name = "گروه اصلی")]
        public int? ParentID { get; set; }
      
        
        [Display(Name = "حذف شده؟")]
        public bool IsRemoved { get; set; }

        #region Rel
        [ForeignKey("ParentID")]
        public List<CourseGroupe> CourseGroupes { get; set; }

        [InverseProperty("ParentCourseGroupe")]
        public List<Course> ParentCourses { get; set; }

        [InverseProperty("SubCourseGroupe")]
        public List<Course> SubCourses { get; set; }
        #endregion

    }
}
