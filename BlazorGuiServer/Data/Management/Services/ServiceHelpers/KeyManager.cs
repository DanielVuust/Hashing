using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hashing
{
    public class KeyManager
    {
        public byte[] CreateKey(int byteLength = 32)
        {
            byte[] key = new byte[byteLength];
            RandomNumberGenerator.Create().GetBytes(key);

            return key;
        }
    }
}
