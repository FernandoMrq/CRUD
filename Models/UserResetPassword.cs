﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Models
{
    public class UserResetPassword : User
    {
        public string SenhaNova { get; set; }
        public string Token { get; set; }
    }
}
