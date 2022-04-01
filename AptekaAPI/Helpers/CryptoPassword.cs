using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AptekaAPI.Helpers
{
    public static class CryptoPassword
    {
        /// <summary>
        /// parollarni SHA384 algoritmi bilan hashlaydi.
        /// </summary>
        /// <param name="password"></param>
        /// <returns>
        /// SHA384 algoritmi bilan hashlangan parollarni qaytaradi.
        /// </returns>
        public static string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }


        /// <summary>
        /// SHA384 algoritmi bilan berilgan parol va hashParolni tekshiradi
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hashPassword"></param>
        /// <returns>bool qaytaradi</returns>
        public static bool Verify(string password, string hashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashPassword);
        }
    }
}
