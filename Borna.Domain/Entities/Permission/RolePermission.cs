using Borna.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borna.Domain.Entities.Permission
{
  public  class RolePermission
    {
        [Key]
        public int RP_ID { get; set; }

        public int RoleID { get; set; }
        public int PermissionID { get; set; }

        #region Rel
        public Permission Permission { get; set; }
        public Role Role { get; set; }
        #endregion
    }
}
