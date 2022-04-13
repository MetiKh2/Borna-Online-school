using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borna.Application.DTOs.UserViewModels
{
   public class ResetPasswordViewModel
    {
        public int UserID { get; set; }

        public string Password { get; set; }
        public string  RePassword { get; set; }

        public string ActivationCode { get; set; }
    }
}
