using Borna.Application.DTOs.UserViewModels;
using Borna.Application.Interfaces;
using Borna.Common.Convertors;
using Borna.Common.Generators;
using Borna.Common.Security;
using Borna.Domain.Entities.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Borna.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IDataBaseContext _context;
        public UserService(IDataBaseContext context)
        {
            _context = context;
        }

        public bool ActivateUser(string ActivationCode)
        {
            var user = _context.Users.Where(p => p.ActivationCode == ActivationCode).SingleOrDefault();
            if (user == null || user.IsActive)
            {
                return false;
            }
            user.IsActive = true;
            user.ActivationCode = GeneratCode.GenerateUniqCode();
            _context.Users.Update(user);
            _context.SaveChanges();
            return true;
        }

        public int AddUserForAdmin(User user, IFormFile Image)
        {
            user.ActivationCode = GeneratCode.GenerateUniqCode();
            user.IsActive = true;
            user.IsRemoved = false;
            user.UserAvatar = "Default.jpg";
            user.Password = MyPasswordHasher.EncodePasswordMd5(user.Password);
            if (Image != null)
            {

                user.UserAvatar = GeneratCode.GenerateUniqCode() + Path.GetExtension(Image.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatars", user.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    Image.CopyTo(stream);
                }
            }
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserID;
        }

        public bool DeleteUser(int userId)
        {
            var user = _context.Users.Find(userId);
            try
            {
                user.IsRemoved = true; user.RemovedDate = DateTime.Now;
                _context.Users.Update(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public void EditPassword(string password, string username)
        {
            var user = GetUserByUserName(username);
            var hashPassword = MyPasswordHasher.EncodePasswordMd5(password);
            user.Password = hashPassword;
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void EditProfile(IFormFile image, string fullname, string username)
        {
            var user = GetUserByUserName(username);
            user.FullName = fullname;

            if (image != null)
            {
                if (user.UserAvatar != "Default.jpg")
                {
                    var pathDelete = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatars", user.UserAvatar);
                    if (File.Exists(pathDelete))
                    {
                        File.Delete(pathDelete);
                    }
                }
                user.UserAvatar = GeneratCode.GenerateUniqCode() + Path.GetExtension(image.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatars", user.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void EditUser(User user, IFormFile Image,string password)
        {
            if (password!= null)
            {
                user.Password = MyPasswordHasher.EncodePasswordMd5(password);
            }
            
            if (Image != null)
            {
                if (user.UserAvatar != "Default.jpg")
                {
                    var pathDelete = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatars", user.UserAvatar);
                    if (File.Exists(pathDelete))
                    {
                        File.Delete(pathDelete);
                    }
                }
                user.UserAvatar = GeneratCode.GenerateUniqCode() + Path.GetExtension(Image.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatars", user.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    Image.CopyTo(stream);
                }
            }

            _context.Users.Update(user);
            _context.SaveChanges();

        }

        public bool EmailExist(string email)
        {
            return _context.Users.Any(p => p.Email == email);
        }

        public string GetFulNameByUserName(string username)
        {
            return _context.Users.Where(p => p.UserName == username).Select(p => p.FullName).Single();
        }

        public UserPanelSideBarViewModel GetSideBarData(string username)
        {
            return _context.Users.Where(p => p.UserName == username).Select(p => new UserPanelSideBarViewModel
            {
                Email = p.Email,
                UserName = p.UserName,
                ImageName = p.UserAvatar
            }).SingleOrDefault();
        }

        public User GetUserByActivationCode(string code)
        {
            return _context.Users.SingleOrDefault(p => p.ActivationCode == code);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(p => p.Email == email);
        }

        public User GetUserByID(int id)
        {
            return _context.Users.Find(id);
        }

        public User GetUserByUserName(string username)
        {
            return _context.Users.Where(p => p.UserName == username).SingleOrDefault();
        }

        public int GetUserIDByUserName(string username)
        {
            return _context.Users.Where(p => p.UserName == username).Select(p => p.UserID).SingleOrDefault();

        }

        public Tuple<List<User>, int> GetUsersForAdmin(int pageId = 1, string filtername = "", string filteremail = "")
        {
            int take = 2;
            int skip = (pageId - 1) * take;

            IQueryable<User> Users = _context.Users;
            if (!string.IsNullOrEmpty(filteremail))
            {
                Users = Users.Where(p => p.Email.Contains(filteremail));
                take = 1000000;
            }
            if (!string.IsNullOrEmpty(filtername))
            {
                take = 1000000;
                Users = Users.Where(p => p.UserName.Contains(filtername));
            }


            int pageCount = Users.OrderByDescending(p => p.UserID).Count() / take;
            return Tuple.Create(Users.OrderByDescending(p => p.UserID).Skip(skip).Take(take).ToList(), pageCount);
        }

        public User LoginUser(string emailOrUserName, string password)
        {
            var email = FixText.FixEmail(emailOrUserName);
            var hashedPassword = MyPasswordHasher.EncodePasswordMd5(password);
            var user = _context.Users.Where(p => (p.Password == hashedPassword && p.Email == email) || (p.Password == hashedPassword && p.UserName == emailOrUserName)).SingleOrDefault();
            return user;

        }

        public void RegisterUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void ResetPassword(string password, string activeCode)
        {
            var user = _context.Users.Single(p => p.ActivationCode == activeCode);
            user.Password = MyPasswordHasher.EncodePasswordMd5(password);
            user.ActivationCode = GeneratCode.GenerateUniqCode();
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public bool UserNameExist(string username)
        {
            return _context.Users.Any(p => p.UserName == username);
        }
    }
}
