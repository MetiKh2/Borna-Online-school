using Borna.Application.Interfaces;
using Borna.Domain.Entities.Course;
using Borna.Domain.Entities.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Borna.Site.Controllers
{
    public class PayController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        public PayController(IOrderService orderService, IUserService userService)
        {
            _userService = userService;
            _orderService = orderService;
        }
      [Authorize]
        public IActionResult Index(int price,int courseId,string courseTitle)
        {

            Order order = new Order
            {
                CourseID = courseId
,                SumAmount = price
            ,
                UserID = _userService.GetUserIDByUserName(User.Identity.Name)
            };
            var orderId = _orderService.AddOrder(order);

            var payment = new ZarinpalSandbox.Payment(price);

            var res = payment.PaymentRequest($"خرید دوره {courseTitle}", "https://localhost:44356/Pay/OnlinePayment/" + orderId);

            if (res.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }
            return NotFound();
        }

        public IActionResult OnlinePayment(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                 HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
                 && HttpContext.Request.Query["Authority"] != "")
            {
                var order = _orderService.GetOrderByID(id);

                string authority = HttpContext.Request.Query["Authority"];

                var payment = new ZarinpalSandbox.Payment((int)order.SumAmount);

                var res = payment.Verification(authority).Result;

                if (res.Status==100)
                {
                    UserCourse userCourse = new UserCourse { 
                    UserID=order.UserID,
                    CourseID=order.CourseID
                    };
                    ViewBag.code = res.RefId;
                    ViewBag.IsSuccess = true;
                    order.IsFinaly = true;
                    _orderService.UpdateOrder(order);
                    _orderService.AddUserCourse(userCourse);
                    return View(order.CourseID);
                }
               
            }

                return View("ErrorPayment");
        }
    }
}
