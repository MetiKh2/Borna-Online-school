using Borna.Domain.Entities.Permission;
using Borna.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borna.Application.Interfaces
{
  public  interface IPermissionService
    {
        #region Role
        int AddRole(Role role);
        List<Role> GetRoles(string FilterRole="");
        bool DeleteRole(int roleId);
        Role GetRoleByID(int roleId);
        int EditRole(Role role);
        void AddUserRoles(List<int> selectedRoles,int userId);
        void EditUserRoles(List<int> selectedRoles, int userId);
        List<int> GetRolesByUserId(int userId);
        #endregion

        #region Permission
        List<Permission> GetPermissions();
        void AddRolePermissions(int roleId,List<int> selectedPermissions);
        List<RolePermission> GetRolePermissionsByID(int roleId);
        void EditRolePermissions(int roleId, List<int> selectedPermissions);
        bool CheckPermission(string username,int permissionId);
        #endregion
    }
}
