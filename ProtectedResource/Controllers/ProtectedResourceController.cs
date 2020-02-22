using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientCredentialApp.Controllers
{
	public class ProtectedResourceController : Controller
	{
		[Route("/protected-resource")]
		[Authorize]
		public string Index()
		{
			return "This is a protected resouce.";
		}

		[Route("/unprotected-resource")]
		public string UnProtectedResource()
		{
			return "This is an unprotected resource.";
		}
	}
}