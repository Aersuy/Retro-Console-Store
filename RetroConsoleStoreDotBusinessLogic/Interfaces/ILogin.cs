using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStore.Domain.Model.User;

namespace RetroConsoleStoreDotBusinessLogic.Interfaces
{
    public interface ILogin
    {
        string LoginLogic(UserLoginDTO data);
    }
}
