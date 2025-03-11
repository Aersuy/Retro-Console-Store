using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotBusinessLogic.Interfaces;

namespace RetroConsoleStoreDotBusinessLogic
{
    internal class BL
    {
        public ISessionBL SessionBL()
        {
            return new AuthBL.SessionBL();
        }
    }
}
