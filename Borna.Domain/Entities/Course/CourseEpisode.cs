using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borna.Domain.Entities.Course
{
    public class CourseEpisode
    {
        [Key]
        public int EpisodeID { get; set; }

        [Required]
        public int CourseID { get; set; }

        [Display(Name = "عنوان قسمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string EpisodeTitle { get; set; }
          
        public string EpisodeFile { get; set; }

        [Display(Name = "رایگان؟")]
        public bool IsFree { get; set; }

        public bool IsRemoved { get; set; }

        [Display(Name = "زمان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public TimeSpan EpisodeTime { get; set; }

        #region Rel
        public Course Course { get; set; }
        #endregion

    }
}
