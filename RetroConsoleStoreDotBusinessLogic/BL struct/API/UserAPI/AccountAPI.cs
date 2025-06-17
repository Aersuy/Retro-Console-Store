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
            using(var ctx = new MainContext())
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
        internal List<UserSmall> GetUsersAPI()
        {
            try
            {
                using (var ctx = new MainContext())
                {
                    var users = ctx.Users.ToList();
                    return users.Select(MapToUserSmall).ToList();
                }

            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Error getting users");
                return new List<UserSmall>();
            }
        }
        private UserSmall MapToUserSmall(UDBTablecs user)
        {
            if (user == null) return null;

            return new UserSmall
            {
                Id = user.id,
                Name = user.username,
                Email = user.email,
                Role = user.level,
                CartId = user.UserCartID,
                ImagePath = user.ImagePath,
                LastIp = user.LastIP
            };
        }
        internal UserSmall GetUserByIDAPI(int id)
        {
            try
            {
                using (var ctx = new MainContext())
                {
                    var user = ctx.Users.FirstOrDefault(p => p.id == id);

                    var userS = MapToUserSmall(user);
                    return userS;

                }
            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Error getting user by id//API");
                return null;
            }
        }
    }
}
