using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotDomain.Model.User;

namespace RetroConsoleStoreDotBusinessLogic.Interfaces
{
    public interface IMessaging
    {
        bool SendBanEmailBL(BanReport report);
    }
}
