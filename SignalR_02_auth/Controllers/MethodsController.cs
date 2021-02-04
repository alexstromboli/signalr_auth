using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;

using SignalR_02_auth.Infrastructure;

namespace SignalR_02_auth.Controllers
{
	[Route ("api/methods")]
	[ApiController]
	[Authorize (AuthenticationSchemes = SalonAuthHandler.AuthSchemeName)]
	public class MethodsController : Controller
	{
		protected IHubContext<MessageHub> Hub;

		public MethodsController (IHubContext<MessageHub> Hub)
		{
			this.Hub = Hub;
		}

		[HttpGet ("test")]
		public string Test ()
		{
			string Message = User.Claims.First (c => c.Type == ClaimTypes.Name).Value + ": " +
			                 System.DateTime.Now.ToString ("s");

			return Message;
		}
	}
}
