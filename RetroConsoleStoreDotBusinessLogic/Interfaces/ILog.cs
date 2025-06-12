using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStore.Domain.Model.User;
using RetroConsoleStoreDotDomain.Logs;
using RetroConsoleStoreDotDomain.Model.Misc;
using RetroConsoleStoreDotDomain.Model.Product;
using RetroConsoleStoreDotDomain.Products;

namespace RetroConsoleStoreDotBusinessLogic.Interfaces
{
    public interface ILog
    {
        string AuthLog(UserLoginDTO data);
        string LoginLog(UserLoginDTO data);
        string ProductLog(ProductModelBack data, string Desc);
        List<LogM> GetRecentBL(int? days);

    }
}
