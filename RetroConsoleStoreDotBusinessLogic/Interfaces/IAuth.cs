using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStore.Domain.Model.User;

namespace RetroConsoleStore.BusinessLogic.Interface
{
    public interface IAuth
    {
        string UserAuthLogic(UserLoginDTO data);
    }
}
