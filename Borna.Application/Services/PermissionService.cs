using Borna.Application.Interfaces;
using Borna.Domain.Entities.Permission;
using Borna.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borna.Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IDataBaseContext _context;
        private readonly IUserService _userService;
        public PermissionService(IDataBaseContext context,IUserService userService)
        {
            _userService = userService;
            _context = context;
        }
        public int AddRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role.RoleID;
        }

        public void AddRolePermissions(int roleId, List<int> selectedPermissions)
        {
            foreach (var item in selectedPermissions)
            {
                _context.RolePermission.Add(new RolePermission
                {
                    RoleID = roleId,
                    PermissionID = item
                });
            }
            _context.SaveChanges();
        }

        public void AddUserRoles(List<int> selectedRoles, int userId)
        {
            foreach (var item in selectedRoles)
            {
                _context.UserInRoles.Add(new UserInRole
                {
                    UserID = userId,
                    RoleID = item
                });
            }

            _context.SaveChanges();
        }

        public bool CheckPermission(string username, int permissionId)
        {
            var userId = _userService.GetUserIDByUserName(username);

            var userRoles = _context.UserInRoles.Where(p => p.UserID == userId).Select(p=>p.RoleID).ToList();
            if (!userRoles.Any())
            {
                return false;
            }

            var rolePermissions = _context.RolePermission.Where(p => p.PermissionID == permissionId).Select(p=>p.RoleID).ToList();

            return rolePermissions.Any(p=>userRoles.Contains(p));
        }

        public bool DeleteRole(int roleId)
        {
            try
            {

                var role = _context.Roles.Find(roleId);
                role.IsRemoved = true;
                _context.Roles.Update(role);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public int EditRole(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
            return role.RoleID;
        }

        public void EditRolePermissions(int roleId, List<int> selectedPermissions)
        {
            var LastRolePermissions = _context.RolePermission.Where(p => p.RoleID == roleId).ToList();
            foreach (var item in LastRolePermissions)
            {
                _context.RolePermission.Remove(item);
            }

            AddRolePermissions(roleId, selectedPermissions);
        }

        public void EditUserRoles(List<int> selectedRoles, int userId)
        {
            var lastRoles = _context.UserInRoles.Where(p => p.UserID == userId).ToList();
            foreach (var item in lastRoles)
            {
                _context.UserInRoles.Remove(item);
            }

            AddUserRoles(selectedRoles, userId);
        }

        public List<Permission> GetPermissions()
        {
            return _context.Permission.ToList();
        }

        public Role GetRoleByID(int roleId)
        {
            return _context.Roles.Find(roleId);
        }

        public List<RolePermission> GetRolePermissionsByID(int roleId)
        {
            return _context.RolePermission.Where(p => p.RoleID == roleId).ToList();
        }

        public List<Role> GetRoles(string FilterRole = "")
        {
            IQueryable<Role> Roles = _context.Roles;
            if (!string.IsNullOrEmpty(FilterRole))
            {
                Roles = Roles.Where(p => p.RoleTitle.Contains(FilterRole));
            }
            return Roles.ToList();
        }

        public List<int> GetRolesByUserId(int userId)
        {
            return _context.UserInRoles.Where(p => p.UserID == userId).Select(p => p.RoleID).ToList();
        }
    }
}
