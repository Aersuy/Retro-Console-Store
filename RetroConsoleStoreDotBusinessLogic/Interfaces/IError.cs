﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroConsoleStoreDotBusinessLogic.Interfaces
{
    public interface IError
    {
        void ErrorToDatabase(Exception ex, string Description);
    }
}
