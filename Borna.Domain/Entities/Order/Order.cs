using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borna.Domain.Entities.Order
{
 public   class Order
    {
        [Key]
        public int OrderID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int CourseID { get; set; }
        [Required]
        public int SumAmount { get; set; }


        public bool IsFinaly { get; set; } = false;

        [Required]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        #region Relo
        public User.User User { get; set; }
        #endregion
    }
}
