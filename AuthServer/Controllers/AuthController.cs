using AuthServer.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.Controlers
{
	public class AuthController : Controller
	{
		[HttpGet]
		public IActionResult Login(string returnUrl)
		{
			return View(new LoginViewModel { ReturnUrl = returnUrl });
		}

		[HttpPost]
		public IActionResult Login(LoginViewModel model)
		{
			var succeeded = true;
			if (succeeded)
				return Redirect(model.ReturnUrl);
			return View();
		}
	}
}