using System.Collections.Generic;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace AuthServer
{
	public static class IdentityConfig
	{
		public static IEnumerable<IdentityResource> GetIdentityResources() =>
			new List<IdentityResource>
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
				new IdentityResources.Email()
			};

		public static IEnumerable<ApiResource> GetApis() =>
			new List<ApiResource>
			{
				new ApiResource("protected_resource", "Protected Resource"),
				new ApiResource("protected_resource_2", "Protected Resource 2"),
			};

		public static IEnumerable<Client> GetClients() =>
			new List<Client>
			{
				new Client
				{
					ClientId = "client_id",
					ClientSecrets = { new Secret("client_secret".ToSha256()) },
					AllowedGrantTypes = GrantTypes.ClientCredentials,
					AllowedScopes = {
						"protected_resource"
					}
				},
				new Client
				{
					ClientId = "client_id_mvc",
					ClientSecrets = { new Secret("client_secret_mvc".ToSha256()) },
					RedirectUris = { "https://localhost:7000/signin-oidc" },
					AllowedGrantTypes = GrantTypes.Code,
					AllowedScopes = {
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile,
						IdentityServerConstants.StandardScopes.Email,
						"protected_resource_2"
					}
				}
			};
	}
}