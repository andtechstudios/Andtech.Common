using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Andtech.Common.Http
{

	public class ApiKeyMiddleware
	{
		public static Func<string, bool> TestApiKey { get; set; }

		private readonly RequestDelegate _next;

		public ApiKeyMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			if (TestApiKey != null)
			{
				if (!context.Request.Headers.TryGetValue("ApiKey", out var requestApiKey))
				{
					context.Response.StatusCode = 401;
					await context.Response.WriteAsync("UNAUTHORIZED");
					return;
				}

				if (!TestApiKey(requestApiKey))
				{
					context.Response.StatusCode = 401;
					await context.Response.WriteAsync("UNAUTHORIZED");
					return;
				}
			}

			await _next(context);
		}
	}
}