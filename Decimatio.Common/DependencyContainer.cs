namespace Decimatio.Common
{
    public static class DependencyContainer
    {
        public static void AddCommonDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IQRGeneratorService, QRGeneratorService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IBlobFilesService, BlobFilesService>();
            services.AddScoped<IPDFGeneratorService, PDFGeneratorService>();
            var blobConfig = configuration.GetSection(nameof(BlobContainerConfig)).Get<BlobContainerConfig>();
            services.AddSingleton(blobConfig);

            var emailConfig = configuration.GetSection(nameof(EmailConfig)).Get<EmailConfig>();
            services.AddSingleton(emailConfig);

            var encryptedConfig = configuration.GetSection(nameof(EncryptedTicketConfig)).Get<EncryptedTicketConfig>();
            services.AddSingleton(encryptedConfig);
        }
    }
}
