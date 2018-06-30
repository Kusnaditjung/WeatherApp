using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace WeatherApp.Web
{
	public class WeatherAppExceptionFilter : IExceptionFilter
	{
		private readonly ILogger _logger;
		private readonly IHostingEnvironment _environment;

		public WeatherAppExceptionFilter(ILoggerFactory logger, IHostingEnvironment environment)
		{
			this._logger = logger?.CreateLogger("WeatherAppException") ?? throw new ArgumentNullException(nameof(logger));
			this._environment = environment ?? throw new ArgumentNullException(nameof(environment));
		}

		public void OnException(ExceptionContext context)
		{
			context.ExceptionHandled = true;
			this.HandlingExceptionForProduction(context);
		}

		private void HandlingExceptionForProduction(ExceptionContext context)
		{
			string requestId = context
				.HttpContext?
				.TraceIdentifier;

			string actionName = context.Exception is HttpRequestException
				? "OpenWeatherServiceDown"
				: "Exception";

			string logParameter = actionName == "Exception" ? string.Empty : "Open Weather Service may be down";

			this._logger.LogError(context.Exception, $"WeatherAppException: RequestID- {requestId}", logParameter);
			context.Result = new RedirectToActionResult(actionName, "Error", new { traceId = requestId });
		}
	}
}
