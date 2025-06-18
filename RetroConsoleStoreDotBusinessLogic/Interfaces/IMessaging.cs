using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotDomain.Model.Misc;
using RetroConsoleStoreDotDomain.Model.User;

namespace RetroConsoleStoreDotBusinessLogic.Interfaces
{
    public interface IMessaging
    {
        bool SendBanEmailBL(BanReport report);
        bool SendConfirmationEmailBl(OTPRequest request);
        bool Send2FAEmailBL(OTPRequest request);
        bool SendContactMessageBL(ContactMSG msg);
        IEnumerable<ContactMSG> GetAllMessagesBL(); 
    }
}
