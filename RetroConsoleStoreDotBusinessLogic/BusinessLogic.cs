using RetroConsoleStore.BusinessLogic.BL_Struct;
using RetroConsoleStore.BusinessLogic.Interface;
using RetroConsoleStoreDotBusinessLogic.BL_struct;
using RetroConsoleStoreDotBusinessLogic.BL_struct.BL.Misc;
using RetroConsoleStoreDotBusinessLogic.BL_struct.BL.Products;
using RetroConsoleStoreDotBusinessLogic.BL_struct.BL.User;
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
        private readonly IStatistics _statisticsBL;
        private readonly IPasswordHash _passwordHash;
        private readonly ILog _loggingBL;
        private readonly IMessaging _messaging;
        private readonly IProductBL _productBL;
        private readonly IAccountBL _accountBL;
        private readonly ICart _cart;
        private readonly IAdmin _adminBL;
        private readonly IReview _review;

        public BusinessLogic()
        {
            _errorBL = new ErrorBL();
            _loggingBL = new Logs(_errorBL); 
            _passwordHash = new PasswordHash();
            _authBL = new AuthBL(_passwordHash,_errorBL,_loggingBL,_statisticsBL);
            _loginBL = new LoginBL1(_passwordHash,_loggingBL,_errorBL);
            _messaging = new MessaginBL(_errorBL);
            _productBL = new ProductBL(_errorBL,_loggingBL);
            _accountBL = new AccountBL(_errorBL);
            _cart = new CartBL(_errorBL, _loggingBL,_loginBL,_statisticsBL);
            _adminBL = new AdminBL(_errorBL, _loginBL,_messaging);
            _statisticsBL = new StatisticsBL(_errorBL, _adminBL);
            _review = new ReviewBL(_errorBL);
        }

        public IAuth GetAuthBL()
        {
            return _authBL;
        }
        public IStatistics GetStatsBL()
        {
            return _statisticsBL;
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
        public IAdmin GetAdminBl()
        {
            return _adminBL;
        }
        public IReview GetReviewBl()
        {
            return _review;
        }
        public ILog GetLogBL()
        {
            return _loggingBL;
        }
    }
}
