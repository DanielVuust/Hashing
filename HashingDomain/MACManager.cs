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
        private readonly Dictionary<string, HMAC> supportedHmacs = new Dictionary<string, HMAC>()
        {
            {"SHA1", new HMACSHA1()},
            {"MD5", new HMACMD5()},
            {"SHA256", new HMACSHA256()},
            {"SHA384", new HMACSHA384()},
            {"SHA512", new HMACSHA512()},
        };
        public HMAC SelectHmac(string hmacName)
        {
            if (!supportedHmacs.ContainsKey(hmacName))
            {
                throw new NotSupportedException($"{hmacName} is not supported");
            }
            return supportedHmacs[hmacName];
        }

        public List<String> GetSupportedHmacs()
        {
            return supportedHmacs.Keys.ToList();
        }
    }
}
