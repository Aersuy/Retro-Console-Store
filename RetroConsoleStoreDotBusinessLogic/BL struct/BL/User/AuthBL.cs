using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using RetroConsoleStoreDotBusinessLogic.BL_struct.UserAPI;


namespace RetroConsoleStore.BusinessLogic.BL_Struct
{     // Use the presentation layer model 
      // Use auto object mapping for translation
      // between the presentation object and the BL
      // object
    public class AuthBL : AuthAPI,  IAuth
    {  
        private readonly IPasswordHash _passwordHash;
        private readonly IError _error;
        private readonly ILog _log;
        public AuthBL(IPasswordHash passwordHash, IError error, ILog log) : base(error, log, passwordHash)
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
            return UserAuthLogicAPI(data);
        }
    }
}