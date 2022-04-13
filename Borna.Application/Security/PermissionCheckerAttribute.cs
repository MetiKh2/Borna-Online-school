using Borna.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borna.Application.Security
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private  IPermissionService _permissionService;
        public int _permissionId = 0;
        public PermissionCheckerAttribute(int permissionId)
        {
            _permissionId = permissionId;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _permissionService = (IPermissionService)context.HttpContext.RequestServices.GetService(typeof (IPermissionService));
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string userName = context.HttpContext.User.Identity.Name;
                if (!_permissionService.CheckPermission(userName,_permissionId))
                {
                    context.HttpContext.Response.Redirect("/login");
                }
            }
            else
            {
                context.HttpContext.Response.Redirect("/login");
            }
        }
    }
}
