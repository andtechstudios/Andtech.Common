using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
					await context.Response.WriteAsync(string.Empty);
					return;
				}

				if (!TestApiKey(requestApiKey))
				{
					context.Response.StatusCode = 401;
					await context.Response.WriteAsync(string.Empty);
					return;
				}
			}

			await _next(context);
		}
	}
}