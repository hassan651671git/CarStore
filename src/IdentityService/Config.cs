using Duende.IdentityServer.Models;

namespace IdentityService;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("auctionApp","Auction app full access"),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
             
            new Client
            {
                ClientId = "postman",
                ClientName="Postman",
                AllowedScopes = { "openid", "profile", "auctionApp" },
                ClientSecrets = { new Secret("NotASecret".Sha256()) },
                RedirectUris = { "https://www.postman.com/oAuth2/callback"},
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword               
            },
        };
}
