using System.Security.Cryptography;

namespace BlazorGuiServer.Data.Services.Managers
{
    public class SymmetricAlgorithmManager
    {
        private readonly List<string> supportedAlgorithms = new List<string>()
        {
            "DES",
            "3DES",
            "SHA256",
            "SHA384",
            "SHA512",
        };
        public SymmetricAlgorithm SelectSymmetricAlgorithmManager(string SymmetricAlgorithmName)
        {
            if (SymmetricAlgorithmName == "DES")
            {
                return DES.Create();
            }
            if (SymmetricAlgorithmName == "3DES")
            {
                return TripleDES.Create();
            }
            if (SymmetricAlgorithmName == "Rijndael")
            {
                return Rijndael.Create();
            }
            throw new NotSupportedException($"{SymmetricAlgorithmName} is not supported");
        }

        public List<string> GetSupportedAlgorithms()
        {
            return supportedAlgorithms;
        }
    }
}
