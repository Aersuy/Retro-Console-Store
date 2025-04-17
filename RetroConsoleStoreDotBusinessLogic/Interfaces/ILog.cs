using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStore.Domain.Model.User;
using RetroConsoleStoreDotDomain.Model.Product;

namespace RetroConsoleStoreDotBusinessLogic.Interfaces
{
    public interface ILog
    {
        string AuthLog(UserLoginDTO data);
        string LoginLog(UserLoginDTO data);
        string ProductLog(ProductModelBack data, string Desc);
    }
}
