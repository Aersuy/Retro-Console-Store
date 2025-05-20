using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Model.User;
using RetroConsoleStoreDotBusinessLogic.BL_struct.UserAPI;
namespace RetroConsoleStoreDotBusinessLogic.BL_struct.BL.User
{
    public class AccountBL : AccountAPI, IAccountBL
    {
        private readonly IError _error;
        public AccountBL(IError error) : base(error) 
        {
            _error = error;
        }

        public string AddProfilePicture(UserSmall model)
        {
            return AddProfilePictureAPI(model);
        }
    }
}
