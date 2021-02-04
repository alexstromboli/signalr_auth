using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace SignalR_02_auth.Infrastructure
{
	internal static class ApiExtensions
	{
		public static IServiceCollection AddApi (this IServiceCollection services)
		{
			services.AddAuthentication (SalonAuthHandler.AuthSchemeName)
				.AddScheme<AuthenticationSchemeOptions, SalonAuthHandler> (SalonAuthHandler.AuthSchemeName, o => { })
				;

			services.AddMvc (o => o.Filters.Add (new EnforceContentTypeFilter ()));
			services.AddControllers ().AddNewtonsoftJson (opt =>
				// remove camel-casing
				(opt.SerializerSettings.ContractResolver as Newtonsoft.Json.Serialization.DefaultContractResolver).NamingStrategy = null
			);

			return services;
		}
	}
}
