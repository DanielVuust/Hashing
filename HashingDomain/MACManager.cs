using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hashing
{
    public class MacManager
    {
        private readonly List<string> supportedHmacs = new List<string>()
        {
            "SHA1",
            "MD5",
            "SHA256",
            "SHA384",
            "SHA512",
        };
        
        public HMAC SelectHmac(string hmacName)
        {
            if (hmacName == "SHA1")
            {
                return new HMACSHA1();
            }
            if (hmacName == "MD5")
            {
                return new HMACMD5();
            }
            if (hmacName == "SHA256")
            {
                return new HMACSHA256();
            }
            if (hmacName == "SHA384")
            {
                return new HMACSHA384();
            }
            if (hmacName == "SHA512")
            {
                return new HMACSHA512();
            }
            throw new NotSupportedException($"{hmacName} is not supported");
        }

        public List<String> GetSupportedHmacs()
        {
            return supportedHmacs;
        }
    }
}
