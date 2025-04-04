using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStore.Domain.Model.User;

namespace RetroConsoleStoreDotBusinessLogic.Interfaces
{
    internal interface ILog
    {
        string AuthLog(UserLoginDTO data);
    }
}
