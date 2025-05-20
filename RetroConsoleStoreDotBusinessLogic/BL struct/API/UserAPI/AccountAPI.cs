using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotBusinessLogic.DBModel;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Model.User;
using RetroConsoleStoreDotDomain.User;

namespace RetroConsoleStoreDotBusinessLogic.BL_struct.UserAPI
{
    public class AccountAPI
    {
        private readonly IError _error;
        internal AccountAPI(IError error)
        {
            _error = error;
        }
        internal string AddProfilePictureAPI(UserSmall model)
        {
            using(var ctx = new UserContext())
            {
                try
                {
                    UDBTablecs existingUser = ctx.Users.FirstOrDefault(u=> u.id == model.Id);
                    if (existingUser == null)
                    {
                        return "Failure";
                    }
                    existingUser.ImagePath = model.ImagePath;
                    ctx.SaveChanges();
                    return null;
                } catch(Exception ex)
                {
                    _error.ErrorToDatabase(ex, "Failure adding profile image");
                    return "Failure";
                }

            }
            
        }
    }
}
