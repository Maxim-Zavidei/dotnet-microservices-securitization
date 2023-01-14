using System.Security.Claims;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;

namespace Jobs.Identity.Config;

public static class InMemoryConfig
{
    public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
    };

    public static IEnumerable<Client> Clients => new List<Client>
    {
        new Client
        {
            ClientId = "first-client",
            ClientSecrets = new [] { new Secret("secret".Sha512()) },
            // Provides the information about the flow we are going to use to deliver the token to the client.
            AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
            AllowedScopes =
            {
                IdentityServerConstants.StandardScopes.OpenId
            }
        }
    };

    public static List<TestUser> TestUsers => new List<TestUser>
    {
        new TestUser
        {
            SubjectId = "1",
            Username = "Bob",
            Password = "password_bob",
            Claims = new List<Claim>
            {
                new Claim("given_name", "Bob"),
                new Claim("family_name", "Bobsen")
            }
        },
        new TestUser
        {
            SubjectId = "2",
            Username = "Steve",
            Password = "password_steve",
            Claims = new List<Claim>
            {
                new Claim("given_name", "Steve"),
                new Claim("family_name", "Stevesen")
            }
        }
    };
}
