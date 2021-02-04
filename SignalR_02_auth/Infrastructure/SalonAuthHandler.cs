using System.Security.Claims;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;

// simple fake authentication middleware

namespace SignalR_02_auth.Infrastructure
{
	internal class SalonAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
	{
		public const string AuthSchemeName = "SalonAuthHandler";
		public static readonly string AuthenticationHeaderName = "Authentication";

		public SalonAuthHandler (IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
			: base (options, logger, encoder, clock)
		{
		}

		protected override Task<AuthenticateResult> HandleAuthenticateAsync ()
		{
			ClaimsPrincipal Principal = null;
			Task<AuthenticateResult> Result = null;

			string AuthHeader = Request.Cookies[AuthenticationHeaderName];

			if (!string.IsNullOrWhiteSpace (AuthHeader))
			{
				if (AuthHeader.StartsWith ("abc_"))
				{
					Claim[] Claims =
					{
						new Claim (ClaimTypes.Name, "Alexey"),
						new Claim (ClaimTypes.NameIdentifier, "1")
					};
					ClaimsIdentity Identity = new ClaimsIdentity (Claims, nameof (SalonAuthHandler));
					Principal = new ClaimsPrincipal (Identity);
				}
				else if (AuthHeader.StartsWith ("xyz_"))
				{
					Claim[] Claims =
					{
						new Claim (ClaimTypes.Name, "Pravin"),
						new Claim (ClaimTypes.NameIdentifier, "2")
					};
					ClaimsIdentity Identity = new ClaimsIdentity (Claims, nameof (SalonAuthHandler));
					Principal = new ClaimsPrincipal (Identity);
				}
			}

			Result = Task.FromResult (
					Principal == null
						? AuthenticateResult.Fail ("not authorized")
						: AuthenticateResult.Success (new AuthenticationTicket (Principal, AuthSchemeName))
				)
				;

			return Result;
		}
	}
}
