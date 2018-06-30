using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace WeatherApp.Web.Controllers
{
	[ExcludeFromCodeCoverage]
	public class ErrorController : Controller
    {
		public IActionResult Exception(string traceId)
		{
			return this.View(model: traceId);
		}

		public IActionResult OpenWeatherServiceDown(string traceId)
		{
			return this.View(model: traceId);
		}

		public IActionResult StatusError(int status)
		{
			return this.View(model: status);
		}

		// Below just for simulation for the inteview, not actually the code for this website
		public IActionResult SimulateException()
		{
			throw new InvalidOperationException("Test Exception");
		}
	}
}
