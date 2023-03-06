using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hashing
{
    internal class MacManager
    {
        public HMAC SelectHmac(string hmacName)
        {
            if (String.Compare(hmacName, "MD5", StringComparison.Ordinal) == 0)
            {
                return new HMACMD5();
            }

            return null;
        }
    }
}
