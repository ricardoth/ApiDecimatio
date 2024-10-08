namespace Decimatio.Domain.MercadoPagoEntitites
{
    public class MercadoPagoNotification
    {
        public string Id { get; set; }
        public string? LiveMode { get; set; }
        public string? Type { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? ApplicationId { get; set; }
        public string? UserId { get; set; }
        public string? Version { get; set; }
        public string? ApiVersion { get; set; }
        public string? Action { get; set; }
        public NotificationData? Data { get; set; }
    }

    public class NotificationData
    {
        public string Id { get; set; }
    }

    public class Payment
    {
        public string Status { get; set; }
        public string StatusDetail { get; set; }
        public double TransactionAmount { get; set; }
        public Payer Payer { get; set; }
        public string PaymentMethodId { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class Payer
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }

}
