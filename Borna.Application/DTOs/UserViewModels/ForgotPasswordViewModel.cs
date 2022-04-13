using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borna.Application.DTOs.UserViewModels
{
   public class ForgotPasswordViewModel
    {
      
        public string Email { get; set; }

        public int UserID { get; set; }
    }
}
