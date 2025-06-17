using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotBusinessLogic.BL_struct.API.MiscAPI;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Model.User;

namespace RetroConsoleStoreDotBusinessLogic.BL_struct.BL.Misc
{
    public class MessaginBL : MessagingAPI, IMessaging
    {
        public MessaginBL(IError error) : base(error)
        {
            
        }
        public bool SendBanEmailBL(BanReport report)
        {
            return SendBanEmail(report);
        }
        public bool SendConfirmationEmailBl(ModifyPasswordRequest request)
        {
            return SendConfirmationEmailAPI(request);
        }
    }
}
