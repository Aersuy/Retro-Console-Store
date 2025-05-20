using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStore.BusinessLogic.BL_Struct;
using RetroConsoleStore.BusinessLogic.Interface;
using RetroConsoleStoreDotBusinessLogic.BL_struct;
using RetroConsoleStoreDotBusinessLogic.BL_struct.BL.User;
using RetroConsoleStoreDotBusinessLogic.BL_struct.ProductsAPI;
using RetroConsoleStoreDotBusinessLogic.BL_struct.UserAPI;
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
        private readonly IProductBL _productBL;
        private readonly IAccountBL _accountBL;
        private readonly ICart _cart;

        public BusinessLogic()
        {
            _errorBL = new ErrorBL();
            _loggingBL = new Logs(_errorBL);
            _passwordHash = new PasswordHash();
            _authBL = new AuthBL(_passwordHash,_errorBL,_loggingBL);
            _loginBL = new LoginBL1(_passwordHash,_loggingBL,_errorBL);
            _productBL = new ProductBL(_errorBL,_loggingBL);
            _accountBL = new AccountBL(_errorBL);
            _cart = new CartBL(_errorBL, _loggingBL,_loginBL);
           
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
            
                return _productBL;
            
        }
        public IAccountBL GetAccountAPI()
        {
            return _accountBL;
        }
        public ICart GetCartBL()
        {
            return _cart;
        }
    }
}
