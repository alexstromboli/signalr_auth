using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SignalR_02_auth.Controllers;
using SignalR_02_auth.Infrastructure;

namespace SignalR_02_auth
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup (IConfiguration Configuration)
		{
			this.Configuration = Configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices (IServiceCollection services)
		{
			/*
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder => builder.WithOrigins("http://signalr_02")
						.AllowAnyMethod()
						.AllowAnyHeader()
						.AllowCredentials());
			});
			*/

			services.AddSignalR ();
			services.AddApi ();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure (IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseRouting ();
			//app.UseCors ("CorsPolicy");
			app.UseAuthentication ();
			app.UseAuthorization ();

			app.UseEndpoints (endpoints =>
			{
				endpoints.MapControllers ();
				endpoints.MapHub<MessageHub> ("/api/messages");
			});
		}
	}
}
