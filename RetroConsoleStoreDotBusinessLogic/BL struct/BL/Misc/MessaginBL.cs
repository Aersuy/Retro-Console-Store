using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotBusinessLogic.BL_struct.API.MiscAPI;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStoreDotDomain.Model.Misc;
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
        public bool SendConfirmationEmailBl(OTPRequest request)
        {
            return SendConfirmationEmailAPI(request);
        }
        public bool Send2FAEmailBL(OTPRequest request)
        {
            return Send2FAEmailAPI(request);
        }
        public bool SendContactMessageBL(ContactMSG msg)
        {
            return SendContactMessageAPI(msg);
        }
        public IEnumerable<ContactMSG> GetAllMessagesBL()
        {
            return GetAllMessagesAPI();
        }
    }
}
