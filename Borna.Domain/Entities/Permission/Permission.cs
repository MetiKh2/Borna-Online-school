using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borna.Domain.Entities.Permission
{
   public class Permission
    {
        [Key]
        public int PermissionID { get; set; }

        [Display(Name = "عنوان دسترسی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string PermissionTitle { get; set; }


        public int? ParentID { get; set; }

        #region Rel
        [ForeignKey("ParentID")]
        public List<Permission> Permissions { get; set; }

        public List<RolePermission> RolePermissions { get; set; }
        #endregion
    }
}
