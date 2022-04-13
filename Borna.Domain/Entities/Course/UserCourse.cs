using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borna.Domain.Entities.Course
{
   public class UserCourse
    {
        [Key]
        public int UC_ID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int CourseID { get; set; }

        #region Rel
        public User.User User { get; set; }
        public Course Course { get; set; }
        #endregion
    }
}
