namespace Common.Models;

public class IdentitySettingsModel
{
    public string IssuerUrl { get; set; }

    public string Audience { get; set; }

    public string ClientId { get; set; }

    public string ClientSecret { get; set; }
}