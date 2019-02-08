using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using session.Logic.Domain;
using session.Logic.Repository;
using session.Logic.Services;
using Steeltoe.Discovery.Client;

namespace voucherz.sessionMgmnt
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors();
			services.AddDiscoveryClient(Configuration);
			services.AddScoped<IBaseDAO<AuthToken>, BaseDAO<AuthToken>>();
			services.AddScoped<IBaseDAO<SessionSetup>, BaseDAO<SessionSetup>>();
			services.AddScoped<ITokenHandler, TokenHandler>();
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				//app.UseHsts();
			}
			app.UseCors(options => options
			.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader()
			);
			app.UseDiscoveryClient();

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
