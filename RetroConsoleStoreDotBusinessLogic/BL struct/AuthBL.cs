using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStore.BusinessLogic.Core;
using RetroConsoleStore.BusinessLogic.Interface;
using RetroConsoleStore.Domain.Model.User;

namespace RetroConsoleStore.BusinessLogic.BL_Struct
{
    public class AuthBL : UserApi, IAuth
    {
        public string UserAuthLogic(UserLoginDTO data)
        {
            throw new NotImplementedException();
        }
    }
}
