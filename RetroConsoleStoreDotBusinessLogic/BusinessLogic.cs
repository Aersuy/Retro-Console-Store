using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStore.BusinessLogic.BL_Struct;
using RetroConsoleStore.BusinessLogic.Interface;

namespace RetroConsoleStore.BusinessLogic
{
    public class BusinessLogic
    {
        private readonly IAuth _authBL;

        public BusinessLogic()
        {
            _authBL = new AuthBL();
        }

        public IAuth GetAuthBL()
        {
            return _authBL;
        }
    }
}
