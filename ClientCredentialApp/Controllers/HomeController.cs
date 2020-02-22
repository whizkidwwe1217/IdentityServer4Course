using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace ClientCredentialApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly IHttpClientFactory httpClientFactory;

		public HomeController(IHttpClientFactory httpClientFactory)
		{
			this.httpClientFactory = httpClientFactory;
		}

		[Route("/")]
		public async Task<IActionResult> Index()
		{
			var serverClient = httpClientFactory.CreateClient();
			var discoverDocument = await serverClient.GetDiscoveryDocumentAsync("https://localhost:5001");
			var tokenResponse = await serverClient.RequestClientCredentialsTokenAsync(
				new ClientCredentialsTokenRequest
				{
					Address = discoverDocument.TokenEndpoint,
					ClientId = "client_id",
					ClientSecret = "client_secret",
					Scope = "protected_resource"
				});
			var apiClient = httpClientFactory.CreateClient();
			apiClient.SetBearerToken(tokenResponse.AccessToken);
			var response = await apiClient.GetAsync("https://localhost:3000/protected-resource");
			var content = await response.Content.ReadAsStringAsync();

			return Ok(new
			{
				access_token = tokenResponse.AccessToken,
				message = content
			});
		}
	}
}