using Borna.Application.Interfaces;
using Borna.Domain.Entities.Course;
using Borna.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borna.Application.Services
{
   public class OrderService: IOrderService
    {
        private readonly IDataBaseContext _context;
        public OrderService(IDataBaseContext context)
        {
            _context = context;
        }

        public int AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order.OrderID;
        }

        public void AddUserCourse(UserCourse userCourse)
        {
            _context.UserCourses.Add(userCourse);
            _context.SaveChanges();
        }

        public Order GetOrderByID(int id)
        {
            return _context.Orders.Find(id);
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }
    }
}
