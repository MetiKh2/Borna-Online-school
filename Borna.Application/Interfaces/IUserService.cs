using Borna.Application.DTOs.UserViewModels;
using Borna.Domain.Entities.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borna.Application.Interfaces
{
    public interface IUserService
    {
        #region Register
        bool UserNameExist(string username);
        bool EmailExist(string email);
        void RegisterUser(User user);

        bool ActivateUser(string ActivationCode);
        #endregion

        #region Login
        User LoginUser(string emailOrUserName,string password);
        void ResetPassword(string password, string activeCode);

        #endregion
        #region Public
        User GetUserByEmail(string email);
        User GetUserByActivationCode(string code);
        User GetUserByUserName(string username);
        int GetUserIDByUserName(string username);
        User GetUserByID(int id);
        #endregion

        #region UserPanel
        string GetFulNameByUserName(string username);
        void EditProfile(IFormFile image,string fullname,string username);
        UserPanelSideBarViewModel GetSideBarData(string username);
        void EditPassword(string password,string username);

        #endregion

        #region Admin
        int AddUserForAdmin(User user,IFormFile Image);
      Tuple<List<User>,int> GetUsersForAdmin(int pageId=1,string filtername="",string filteremail="");
        bool DeleteUser(int userId);
        void EditUser(User user,IFormFile Image,string password);

        #endregion

    }
}
