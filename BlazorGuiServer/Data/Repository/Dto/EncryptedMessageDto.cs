namespace BlazorGuiServer.Data.Repository.Dto
{
    public class EncryptedMessageDto
    {
        public byte[] EncryptedMessage { get; set; }
        public byte[] EncryptionKey { get; set; }
        public byte[] EncryptionIv { get; set; }
    }
}
