﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotDomain.Enums;

namespace RetroConsoleStore.Domain.Model.User
{
    public class UserLoginDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserIp { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
