﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetroConsoleStoreDotWeb.Models.Auth
{
    public class Auth
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string SessionId { get; set; }
    }
}