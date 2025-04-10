using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetroConsoleStoreDotWeb.Models.UserVisible
{
    public class UserOutput
    {

        public string Name { get; set; }   
        public string Email { get; set; }
        public bool Gender { get; set; }
        public UInt64 GenderB {  get; set; }

        public string RegisterData { get; set; }
        public string ProfilePicturePath {  get; set; }

    }
}