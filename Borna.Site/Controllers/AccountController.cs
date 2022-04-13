using Borna.Application.DTOs.UserViewModels;
using Borna.Application.Interfaces;
using Borna.Common.Convertors;
using Borna.Common.Generators;
using Borna.Common.Security;
using Borna.Common.Senders;
using Borna.Domain.Entities.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Borna.Site.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IViewRenderService _viewRenderService;
        public AccountController(IUserService userService, IViewRenderService viewRenderService)
        {
            _viewRenderService = viewRenderService;
            _userService = userService;
        }

        #region Register
        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel register)
        {
          

            if (register.Password != register.RePassword)
            {
                ModelState.AddModelError("RePassword", "کلمه عبور و تکرار کلمه عبور یکسان نیستند ");
                return View(register);
            }

            if (_userService.UserNameExist(register.UserName))
            {
                ModelState.AddModelError("UserName", " نام کاربری قبلا مورد استفاده قرار گرفته است");
                return View(register);
            }
            if (_userService.EmailExist(register.Email))
            {
                ModelState.AddModelError("Email", " ایمیل  قبلا مورد استفاده قرار گرفته است");
                return View(register);
            }


            User user = new User
            {
                IsActive = false,
                Email = FixText.FixEmail(register.Email),
                FullName = register.FullName,
                UserName = register.UserName,
                Password = MyPasswordHasher.EncodePasswordMd5(register.Password),
                ActivationCode = GeneratCode.GenerateUniqCode(),
                UserAvatar = "Default.jpg"

            };
            _userService.RegisterUser(user);

            #region SendEmail
            var body = _viewRenderService.RenderToStringAsync("_ActivateEmail",user);
            EmailSender.Send(user.Email, "فعالسازی حساب کاربری",body);
            #endregion
            return View("SuccessRegister",model:register);
        }


        #endregion

        #region Login
        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {

            if (!ModelState.IsValid)
            {
                return View(login);
            }
            var user = _userService.LoginUser(login.UserNameOrEmail,login.Password);
            if (user==null)
            {
                ModelState.AddModelError("Password", "کاربری با این مشخصات در سایت ثبت نام نکرده است ");
                return View(login);
            }

            else
            {
                if (user.IsActive)
                {
                    var Claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserID.ToString()),
                        new Claim(ClaimTypes.Name,user.UserName),
                        new Claim(ClaimTypes.Email,user.Email),

                    };
                    var identity = new ClaimsIdentity(Claims,CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = login.RememberMe,

                    };
                    HttpContext.SignInAsync(principal,properties);
                    ViewBag.IsSeccess = true;
                    return View();
                }
                else
                {
                    ModelState.AddModelError("Password", "حساب این کاربر هنوز فعال نشده است ");
                    return View(login);
                }
            }
           
        }
        #endregion

        #region ActiveUser
        public IActionResult ActiveUser(string id)
        {
            ViewBag.IsActive = _userService.ActivateUser(id);
            return View();
        }
        #endregion

        #region Log out
        [Route("LogOut")]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
        #endregion


        #region ForgotPassword
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(ForgotPasswordViewModel forgot)
        {
            var user = _userService.GetUserByEmail(forgot.Email);
            if (user==null)
            {
                ViewBag.UserNull = true;
                return View(forgot);
            }
            else
            {
                var body = _viewRenderService.RenderToStringAsync("_ForgotPassword",user);
                EmailSender.Send(user.Email, "تغییر کلمه عبور", body);
                return View("ResetPasswordEmail", user);

            }
        }

        
        public IActionResult ResetPassword(string id)
        {
            var user = _userService.GetUserByActivationCode(id);
            if (user==null)
            {
                return BadRequest();
            }
            //  ViewBag.activeCode = user.ActivationCode;

            return View(new ResetPasswordViewModel { ActivationCode = id });

        
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel reset)
        {
            if (reset.Password!=reset.RePassword)
            {
                ModelState.AddModelError("RePassword","رمز عبور و تکرار آن برابر نیستند");
                ViewBag.PasswordRePassword = true;
                return View(reset);
            }
            _userService.ResetPassword(reset.Password,reset.ActivationCode);
            return RedirectToAction("Login");
        }
        #endregion
    }
}
