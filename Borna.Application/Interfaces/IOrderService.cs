using Borna.Domain.Entities.Course;
using Borna.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borna.Application.Interfaces
{
  public  interface IOrderService
    {
        int AddOrder(Order order);
        Order GetOrderByID(int id);
        void UpdateOrder(Order order);
        void AddUserCourse(UserCourse userCourse);
    }
}
