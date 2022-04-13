using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borna.Domain.Entities.Course
{
   public class CourseComment
    {
        [Key]
        public int CommentID { get; set; }

        [Required]
        public int UserID { get; set; }
        [Required]
        public int CourseID { get; set; }

        public string Comment { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public bool IsRemoved { get; set; }

        #region Rel
        public User.User User { get; set; }
        public Course Course { get; set; }

        #endregion

    }
}
