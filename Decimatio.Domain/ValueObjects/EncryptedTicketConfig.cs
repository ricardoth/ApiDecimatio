namespace Decimatio.Domain.ValueObjects
{
    public class EncryptedTicketConfig
    {
        public string? PrivateKey { get; set; }
        public string? VectorKey { get; set; }
    }
}
