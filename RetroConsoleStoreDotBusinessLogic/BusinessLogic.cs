using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStore.BusinessLogic.BL_Struct;
using RetroConsoleStore.BusinessLogic.Interface;
using RetroConsoleStoreDotBusinessLogic.BL_struct;
using RetroConsoleStoreDotBusinessLogic.BL_struct.ProductsAPI;
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
        private readonly IProductAPI _productAPI;
        private readonly IProductBL _productBL;

        public BusinessLogic()
        {
            _errorBL = new ErrorBL();
            _loggingBL = new Logs(_errorBL);
            _passwordHash = new PasswordHash();
            _authBL = new AuthBL(_passwordHash,_errorBL,_loggingBL);
            _loginBL = new LoginBL1();
            _productAPI = new ProductAPI(_errorBL, _loggingBL);
            _productBL = new ProductBL(_errorBL,_loggingBL,_productAPI);
           
        }

        public IAuth GetAuthBL()
        {
            return _authBL;
        }
        
        public ILogin GetLoginBL()
        {
            return _loginBL;
        }
        public IProductBL GetProductBL()
        {
            {
                return _productBL;
            }
        }
    }
}
