using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStore.Domain.Model.User;
using RetroConsoleStoreDotBusinessLogic.DBModel;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Enums;
using RetroConsoleStoreDotDomain.Model.Statistics;
using RetroConsoleStoreDotDomain.User;
using RetroConsoleStoreHelpers.Interfaces;

namespace RetroConsoleStoreDotBusinessLogic.BL_struct.UserAPI
{
    public class AuthAPI
    {
        private readonly IError _error;
        private readonly ILog _log;
        private readonly IPasswordHash _passwordHash;
        private readonly IStatistics _statistics;

        public AuthAPI(IError error, ILog log,IPasswordHash passwordHash, IStatistics statistics)
        {
            _passwordHash = passwordHash;
            _error = error;
            _log = log;
            _statistics = statistics;
            
        }
        internal string UserAuthLogicAPI(UserLoginDTO data)
        {
            using (var ctx = new UserContext())
            {
                try
                {    // Validation
                    if (!(bool)ValidateUserInputAPI(data))
                    {
                        return "Enter valid data";
                    }

                    // New user creation
                    UDBTablecs NewUser = CreateNewUserAPI(data);

                    // Saving

                    //Result
                    var user = ctx.Users.FirstOrDefault(u => u.username == data.UserName);

                    _log.AuthLog(data);

                    return user != null ? "User created and retrieved successfully!" : "Failed to create/retrieve user";
                }

                catch (Exception ex)
                {
                    // Logging and writting exception to DB
                    _log.AuthLog(data);

                    _error.ErrorToDatabase(ex, "Problem with auth process");

                    return $"Database error: {ex.Message}";
                }
            }
        }
        private bool? ValidateUserInputAPI(UserLoginDTO data)
        {
            try
            {
                if (!string.IsNullOrEmpty(data.UserName) && !string.IsNullOrEmpty(data.Password) && data.UserName.Length >= 5 && data.Password.Length >= 8)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Problem with validating input");
                return null;
            }

        }
        /// <summary>
        /// This function returns a nullable bool which is true if a user with
        /// the same name exists, false otherwise and null if the function fail
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        private bool? UserWithNameAlreadyExistsAPI(UserLoginDTO data, UserContext ctx)
        {
            try
            {
                return ctx.Users.Any(u => u.username == data.UserName);

            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Problem with verifying if user with this name already exists" +
                    "most likely input data issue, or the entire program is f-ed");
                return null;
            }

        }
        /// <summary>
        /// Function that creates a new user who's atributes match
        /// the user table in the db using the UserLoginDTO model
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private UDBTablecs CreateNewUserAPI(UserLoginDTO data)
        {
            try
            {

                var NewUser = new UDBTablecs
                {
                    username = data.UserName,
                    password = _passwordHash.HashPassword(data.Password),
                    email = data.Email ?? "default@email.com",
                    RegisterDate = DateTime.Now,
                    LastRegisterDate = DateTime.Now,
                    LastIP = data.UserIp,
                    level = URole.User
                };
                var cart = new UserCartT();
                var newStats = new UserStatsT
                {
                    User = NewUser,
                    totalPagesViewed = 0,
                    totalProductsAddedToCart = 0,
                    totalSpent = 0,
                    totalProductsPurchased = 0,
                    totalTimesLoggedOn = 0,
                };
                

                using (var ctx = new UserContext())
                {
                    try
                    {
                        ctx.UserCarts.Add(cart);
                        ctx.UserStatsTs.Add(newStats);
                        ctx.Users.Add(NewUser);
                        ctx.SaveChanges();

                        NewUser.UserCartID = cart.CartID;
                        cart.UserID = NewUser.id;
                        ctx.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        _error.ErrorToDatabase(ex, "Error saving user and cart");
                        throw;
                    }
                }
                return NewUser;

            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex, "Problem with creating new user");
                return null;
            }

        }
    }
}

