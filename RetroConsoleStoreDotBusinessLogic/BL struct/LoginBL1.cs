using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStore.BusinessLogic.Interface;
using RetroConsoleStore.Domain.Model.User;
using RetroConsoleStoreDotBusinessLogic.DBModel;
using RetroConsoleStoreDotBusinessLogic.Interfaces;

namespace RetroConsoleStoreDotBusinessLogic.BL_struct
{
    public class LoginBL1 : ILogin
    {
        public string LoginLogic(UserLoginDTO data)
        {
            using (var ctx = new UserContext())
            {

            }

        }

        private bool ValidateUserInput(UserLoginDTO data)
        {
            if (!string.IsNullOrEmpty(data.UserName) && !string.IsNullOrEmpty(data.Password) && data.UserName.Length >= 5 && data.Password.Length >= 8)
            {
                return true;
            }
            return false;
        }
    }
}