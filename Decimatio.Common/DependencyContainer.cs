using Decimatio.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;

namespace Decimatio.Common
{
    public static class DependencyContainer
    {
        public static void AddCommonDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IQRGeneratorService, QRGeneratorService>();
            services.AddScoped<IEmailSenderService, EmailSenderService>();
            services.AddScoped<IBlobFilesService, BlobFilesService>();
            services.AddScoped<IPDFGeneratorService, PDFGeneratorService>();

            var emailSenderConfig = configuration.GetSection(nameof(EmailSenderOptions)).Get<EmailSenderOptions>();
            services.AddSingleton(emailSenderConfig);
            
            var blobConfig = configuration.GetSection(nameof(BlobContainerConfig)).Get<BlobContainerConfig>();
            services.AddSingleton(blobConfig);

            var encryptedConfig = configuration.GetSection(nameof(EncryptedTicketConfig)).Get<EncryptedTicketConfig>();
            services.AddSingleton(encryptedConfig);

        }
    }
}
