using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStore.BusinessLogic.BL_Struct;
using RetroConsoleStore.BusinessLogic.Interface;
using RetroConsoleStoreDotBusinessLogic.BL_struct;
using RetroConsoleStoreDotBusinessLogic.Interfaces;

namespace RetroConsoleStore.BusinessLogic
{
    public class BusinessLogic
    {
        private readonly IAuth _authBL;
        private readonly ILogin _loginBL;

        public BusinessLogic()
        {
            _authBL = new AuthBL();
            _loginBL = new LoginBL1();
        }

        public IAuth GetAuthBL()
        {
            return _authBL;
        }
        
        public ILogin GetLoginBL()
        {
            return _loginBL;
        }
    }
}
