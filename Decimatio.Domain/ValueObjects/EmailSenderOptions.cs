namespace Decimatio.Domain.ValueObjects
{
    public class EmailSenderOptions
    {
        public string Url { get; set; }
        public string UserBasicAuth { get; set; }
        public string PassBasicAuth { get; set; }
    }
}
