using System;
namespace Api.Configurations
{
    public class HelpetGeneralSettings : IHelpetGeneralSettings
    {
        public string JwtSecret { get; set; }
        public string AdminJwtSecret { get; set; }
        public string GoogleApiKey { get; set; }
        public string S3AccessKey { get; set; }
        public string S3SecretKey { get; set; }
        public int ThumbnailSize { get; set; }
    }

    public interface IHelpetGeneralSettings
    {
        string JwtSecret { get; set; }
        string AdminJwtSecret { get; set; }
        string GoogleApiKey { get; set; }
        public string S3AccessKey { get; set; }
        public string S3SecretKey { get; set; }
        public int ThumbnailSize { get; set; }
    }
}
