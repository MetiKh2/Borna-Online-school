﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borna.Common.Convertors
{
  public static  class FixText
    {
        
        public static string FixEmail(string email)
        {
            return email.Trim().ToLower();
        }
    }
}
