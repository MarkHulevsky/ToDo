using IdentityServer4.Models;

namespace Identity.Core.Configuration;

public static class IdentityServerConfiguration
{
    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new()
            {
                ClientId = "unauthorized_client_id",
                ClientSecrets = { new Secret("client_secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "UsersAPI" }
            },
            new()
            {
                ClientId = "authorized_client_id",
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedScopes =
                {
                    "openid",
                    "TodoAPI",
                    "MailAPI",
                    "UsersAPI"
                },
                AllowAccessTokensViaBrowser = true,
                RequireConsent = false
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new("TodoAPI"),
            new("MailAPI"),
            new("UsersAPI")
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
            new("TodoAPI")
            {
                Scopes = { "TodoAPI" },
                ApiSecrets = { new Secret("todoApi_secret".Sha256()) }
            },
            new("MailAPI")
            {
                Scopes = { "MailAPI" },
                ApiSecrets = { new Secret("mailAPI_secret".Sha256()) }
            },
            new("UsersAPI")
            {
                Scopes = { "UsersAPI" },
                ApiSecrets = { new Secret("mailAPI_secret".Sha256()) }
            }
        };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
}