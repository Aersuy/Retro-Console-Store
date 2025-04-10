using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStore.BusinessLogic.Core;
using RetroConsoleStore.BusinessLogic.Interface;
using RetroConsoleStore.Domain.Model.User;
using RetroConsoleStoreDotBusinessLogic.DBModel;
using RetroConsoleStoreDotDomain.User;
using RetroConsoleStoreDotDomain.Enums;
using System.Runtime.InteropServices.ComTypes;
using RetroConsoleStoreDotBusinessLogic.BL_struct;
using RetroConsoleStoreHelpers.PasswordHash;
using RetroConsoleStoreHelpers.Interfaces;
using RetroConsoleStoreDotBusinessLogic.Interfaces;


namespace RetroConsoleStore.BusinessLogic.BL_Struct
{     // Use the presentation layer model 
      // Use auto object mapping for translation
      // between the presentation object and the BL
      // object
    public class AuthBL : UserApi, IAuth
    {  
        private readonly IPasswordHash _passwordHash;
        private readonly IError _error;
        private readonly ILog _log;
        public AuthBL(IPasswordHash passwordHash, IError error, ILog log)
        {
            _error = error;
            _passwordHash = passwordHash;
            _log = log;
        }
        /// <summary>
        /// Main auth logic for website, first validates user input
        /// then checks if a user with the username already exists
        /// if it doesn't, It creates a new user using the data taken
        /// from the input model, then it verifies if everything went properly
        /// and logs the result.
        /// If the function fails it thows an exception which is written to the 
        /// database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string UserAuthLogic(UserLoginDTO data)
        {
            using (var ctx = new UserContext())
            {
                try
                {    // Validation
                    if (!(bool)ValidateUserInput(data))
                    {
                        return "Enter valid data";
                    }
                    if((bool)UserWithNameAlreadyExists(data,ctx))
                    {
                        return "User with name already exists";
                    }
                     // New user creation
                    UDBTablecs NewUser = CreateNewUser(data);
                    
                    // Saving
                    ctx.Users.Add(NewUser);
                    ctx.SaveChanges();

                    //Result
                    var user = ctx.Users.FirstOrDefault(u => u.username == data.UserName);
                    
                    _log.AuthLog(data);
                    
                    return user != null ? "User created and retrieved successfully!" : "Failed to create/retrieve user";
                }

                catch (Exception ex)
                {
                     // Logging and writting exception to DB
                    _log.AuthLog(data);

                    _error.ErrorToDatabase(ex,"Problem with auth process");
                    
                    return $"Database error: {ex.Message}";
                }
            }
        }

        /// <summary>
        /// Validates user input, checks if the name or pass does not exit
        /// Verifies Username or password length (>5 >8)
        /// If everything checks out return nullable bool with it being 
        /// Null if the function fails.
        /// </summary>
        private bool? ValidateUserInput(UserLoginDTO data)
        {
            try {
                if (!string.IsNullOrEmpty(data.UserName) && !string.IsNullOrEmpty(data.Password) && data.UserName.Length >= 5 && data.Password.Length >= 8)
                {
                    return true;
                }
                return false;
            } catch (Exception ex)
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
        private bool? UserWithNameAlreadyExists(UserLoginDTO data, UserContext ctx)
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
        private UDBTablecs CreateNewUser(UserLoginDTO data)
        {   try
            {
                return new UDBTablecs
                {
                    username = data.UserName,
                    password = _passwordHash.HashPassword(data.Password),
                    email = data.Email ?? "default@email.com",
                    RegisterDate = DateTime.Now,
                    LastRegisterDate = DateTime.Now,
                    LastIP = data.UserIp,
                    level = URole.User
                };
            }
            catch (Exception ex)
            {
                _error.ErrorToDatabase(ex,"Problem with creating new user");
                return null;
            }
           
        }
    }
}