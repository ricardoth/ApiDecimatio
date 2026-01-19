namespace Decimatio.Domain.ValueObjects
{
    public class BlobContainerConfig
    {
        public string? ConnectionString { get; set; }
        public string? ContainerName { get; set; }
        public string? FolderName { get; set; }
        public string? FolderFlyerName { get; set; }
        public string? ReferencialMapName { get; set; }
        public string? FolderMedioPago { get; set; }
    }
}
