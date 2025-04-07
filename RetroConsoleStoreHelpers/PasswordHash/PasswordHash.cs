using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using RetroConsoleStoreHelpers.Interfaces;
namespace RetroConsoleStoreHelpers.PasswordHash
{
    public class PasswordHash : IPasswordHash
    {
           public string HashPassword(string password)
        {
            try
            {
               string salt = BCrypt.Net.BCrypt.GenerateSalt(10);
               return BCrypt.Net.BCrypt.HashPassword(password,salt);
            }
            catch (Exception)
            {
                throw new Exception("Error accoured while hashing password");
            }

        }
        public bool VerifyPassword(string password, string hash)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(password, hash);

            }
            catch (Exception)
            {
                throw new Exception("Error accoured while verifying password");
            }

        }
    }
}
