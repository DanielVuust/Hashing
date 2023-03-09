using System.Security.Cryptography;

namespace BlazorGuiServer.Data.Services.Managers
{
    public class SymmetricAlgorithmManager
    {
        private readonly List<string> _supportedAlgorithms = new()
        {
            "DES",
            "3DES",
            "AES",
        };
        public SymmetricAlgorithm SelectSymmetricAlgorithmManager(string symmetricAlgorithmName)
        {
            switch (symmetricAlgorithmName)
            {
                case "DES":
                    return (SymmetricAlgorithm)DES.Create();
                case "3DES":
                    return TripleDES.Create();
                case "AES":
                    return Aes.Create();
                default:
                    throw new NotSupportedException($"{symmetricAlgorithmName} is not supported");
            }
        }

        public List<string> GetSupportedAlgorithms()
        {
            return _supportedAlgorithms;
        }
    }
}
