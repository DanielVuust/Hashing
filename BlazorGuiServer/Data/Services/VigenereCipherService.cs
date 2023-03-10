using System.Security.Cryptography.Xml;
using FluentResults;

namespace BlazorGuiServer.Data.Services
{
    public class VigenereCipherService
    {
        public Result<string> Encrypt(string code, string message)
        {
            code = code.ToUpper();
            message = SanitizeInput(message.ToUpper());

            
            string result = string.Empty;
            int messageIndex = 0;
            foreach(char charr in message)
            {
                char codeChar = code[(messageIndex % code.Length)];
                var i = this.ConvertToListIndex(codeChar) + this.ConvertToListIndex(charr);
                result += this.ConvertListIndexToChar(i);


                messageIndex++;
            }

            return Result.Ok(result);
        }
        public Result<string> Decrypt(string code, string encryptedMessage)
        {
            code = code.ToUpper();
            encryptedMessage = encryptedMessage.ToUpper();

            string result = string.Empty;
            int messageIndex = 0;
            foreach (char charr in encryptedMessage)
            {
                char codeChar = code[(messageIndex % code.Length)];
                var i = this.ConvertToListIndex(charr) - this.ConvertToListIndex(codeChar);
                while (i < 0)
                {
                    i += 27;
                }
                result += this.ConvertListIndexToChar(i);


                messageIndex++;
            }

            return Result.Ok(result);
        }
        public int ConvertToListIndex(char c)
        {
            if (c == '_' || c == ' ')
            {
                return 26;
            }
            return Convert.ToInt32(c - 'A');
        }
        public char ConvertListIndexToChar(int c)
        {
            c = c % 27;
            if (c == 26)
            {
                return '_';
            }
            return Convert.ToChar(c + 'A');
        }
        public string SanitizeInput(string input)
        {
            var res = "";
            foreach (var c in input)
            {
                if (c > 90 && c != ' ' || c < 65 && c != ' ')
                {
                    continue;
                }

                if (c == ' ')
                {
                    res += '_';
                    continue;
                }

                res += c;
            }

            return res;
        }

    }
}
