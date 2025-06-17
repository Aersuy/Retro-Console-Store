using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Model.User;
using RetroConsoleStoreDotBusinessLogic.BL_struct.UserAPI;
using System.Collections.Generic;
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
        public List<UserSmall> GetUsersBL()
        {
            return GetUsersAPI();
        }
        public UserSmall GetUserByIDBL(int id) 
        {
            return GetUserByIDAPI(id);
        }
    }
}
