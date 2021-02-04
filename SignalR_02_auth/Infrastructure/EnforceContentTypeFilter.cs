using Microsoft.AspNetCore.Mvc.Filters;

namespace SignalR_02_auth.Infrastructure
{
	// ensure Content-Type is filled in if missing
	class EnforceContentTypeFilter : IResourceFilter
	{
		protected static readonly string ContentTypeHeader = "Content-Type";
		protected static readonly string DefaultContentType = "application/json";
		protected static readonly string CurlDefaultContentType = "application/x-www-form-urlencoded";

		public void OnResourceExecuting (ResourceExecutingContext context)
		{
			if (!context.HttpContext.Request.Headers.ContainsKey (ContentTypeHeader)
				|| context.HttpContext.Request.Headers["User-Agent"].ToString ().StartsWith ("curl/")
					&& context.HttpContext.Request.Headers[ContentTypeHeader] == CurlDefaultContentType
				)
			{
				context.HttpContext.Request.Headers[ContentTypeHeader] = DefaultContentType;
			}
		}

		public void OnResourceExecuted (ResourceExecutedContext context)
		{
		}
	}
}
