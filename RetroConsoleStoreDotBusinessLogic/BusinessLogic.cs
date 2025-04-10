using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStore.BusinessLogic.BL_Struct;
using RetroConsoleStore.BusinessLogic.Interface;
using RetroConsoleStoreDotBusinessLogic.BL_struct;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreHelpers.Interfaces;
using RetroConsoleStoreHelpers.PasswordHash;
namespace RetroConsoleStore.BusinessLogic
{
    public class BusinessLogic
    {
        private readonly IAuth _authBL;
        private readonly ILogin _loginBL;
        private readonly IError _errorBL;
        private readonly IPasswordHash _passwordHash;
        private readonly ILog _loggingBL;

        public BusinessLogic()
        {
            _loggingBL = new Logs();
            _errorBL = new ErrorBL();
            _passwordHash = new PasswordHash();
            _authBL = new AuthBL(_passwordHash,_errorBL,_loggingBL);
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
