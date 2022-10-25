using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Mongo.Services.Identity
{
    public static class SD
    {
        public static string Admin = "Admin";
        public static string Customer = "Customer";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope(name: "mongo",   displayName: "Mongo Server"),
                new ApiScope(name: "read",   displayName: "Read your data."),
                new ApiScope(name: "write",  displayName: "Write your data."),
                new ApiScope(name: "delete", displayName: "Delete your data.")
            };

        public static IEnumerable<Client> Clients =>
           new List<Client>
           {
                new Client
                {
                    ClientId="client",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    AllowedScopes={"read","write","profile"}
                },
                new Client
                {
                    ClientId="mongo",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes=GrantTypes.Code,//khi nào login or logout successfully
                    RedirectUris={ "https://localhost:44350/signin-oidc" },
                    PostLogoutRedirectUris={ "https://localhost:44350/signout-callback-oidc" },
                    AllowedScopes=new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "mongo"
                    }
                },
           };

    }
}
