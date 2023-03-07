using System.Security.Cryptography;

namespace Hashing
{
    internal class Program
    {
        private static byte[] key;
        static void Main(string[] args)
        {
            while (true)
            {

                //Creates random key.
                KeyManager km = new KeyManager();
                key = km.CreateKey();

                Console.WriteLine();
                Console.WriteLine("Select hash type");
                string? hashName = Console.ReadLine();

                MacManager macManager = new MacManager();
                HMAC hmac = macManager.SelectHmac(hashName);

                Console.WriteLine("Text to encrypt");
                string? text = Console.ReadLine();
                Encryptor encryptor = new Encryptor();
                string encrypedText = encryptor.Encrypt(hmac, key, text);


                DisplayInfo(hashName, text, encrypedText);

                Console.WriteLine("");
            }

        }

        private static void DisplayInfo(string hashType, string plainText, string macAscii)
        {
            Console.Clear();
            Console.WriteLine($"Hash Type: {hashType}");
            Console.WriteLine();
            Console.WriteLine($"Key: {Convert.ToBase64String(key)}");
            Console.WriteLine();
            Console.WriteLine($"Plain text ASCII: {plainText}");
            Console.WriteLine();
            Console.WriteLine($"MAC ASCII: {macAscii}");
            //Console.WriteLine($"MAC HEX: {macHex}");


        }
    }
}