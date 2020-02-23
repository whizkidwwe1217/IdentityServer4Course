using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AuthServer
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.ConfigureApplicationCookie(config =>
			{
				config.Cookie.Name = "IdentityServer.Cookie";
				config.LoginPath = "/Auth/Login";
			});

			services.AddIdentityServer(options =>
				{
					options.UserInteraction = new IdentityServer4.Configuration.UserInteractionOptions
					{
						LoginUrl = "/Auth/Login"
					};
				})
				.AddInMemoryApiResources(IdentityConfig.GetApis())
				.AddInMemoryClients(IdentityConfig.GetClients())
				.AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources())
				.AddDeveloperSigningCredential();
			services.AddControllersWithViews();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseIdentityServer();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapDefaultControllerRoute();
			});
		}
	}
}
