using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroConsoleStoreDotBusinessLogic.BL_struct.ProductsAPI;
using RetroConsoleStoreDotBusinessLogic.Interfaces;

namespace RetroConsoleStoreDotBusinessLogic.BL_struct
{
    public class CartBL : CartAPI, ICartAPI
    {
        private readonly IError _error;
        private readonly ILog _log;
        private readonly ILogin _login;
        public CartBL(IError error, ILog log, ILogin login)
        {

        }
    }
}
