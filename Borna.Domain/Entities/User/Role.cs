using Borna.Domain.Entities.Permission;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borna.Domain.Entities.User
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [Display(Name = "نام نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string RoleTitle { get; set; }

        [Display(Name = "حدف شده؟")]
        public bool IsRemoved { get; set; } = false;

        #region Rel
        public List<UserInRole> UserInRoles { get; set; }
        public List<RolePermission> RolePermissions { get; set; }

        #endregion
    }
}
