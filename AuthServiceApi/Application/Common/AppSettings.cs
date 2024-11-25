namespace AuthServiceApi.Application.Common
{
    public class AppSettings
    {
        public ApplicationDetail? ApplicationDetail { get; set; }
        public ConnectionStrings? ConnectionStrings { get; set; }
        public Logging? Logging { get; set; }
        public Jwt? Jwt { get; set; }
        public MailConfigurations? MailConfigurations { get; set; }
        public bool UseInMemoryDatabase { get; set; }
        public string[]? Cors { get; set; }
    }
    public class ApplicationDetail
    {
        public string ApplicationName { get; set; }=string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ContactWebsite { get; set; } = string.Empty;
    }

    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; } = string.Empty;
    }
    public class Jwt
    {
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
    }

    public class MailConfigurations
    {
        public string From { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int Port { get; set; } 
    }
    public class Logging
    {
        public RequestResponse? RequestResponse { get; set; }
    }

    public class RequestResponse
    {
        public bool IsEnabled { get; set; }
    }


}
