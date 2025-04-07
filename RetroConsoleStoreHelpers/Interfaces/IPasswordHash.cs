using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroConsoleStoreHelpers.Interfaces
{
    public interface IPasswordHash
    {
         string HashPassword(string password);
         bool VerifyPassword(string password, string hash);
    }
}
