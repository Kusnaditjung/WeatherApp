using Microsoft.AspNetCore.Mvc;
using System;
using WeatherApp.Common;
using WeatherApp.Web.Commands;
using WeatherApp.Web.Controllers;
using WeatherApp.Web.Models;
using Xunit;

namespace WeatherApp.Tests.Web.Commands
{
    public class CitySearchCommandTest
    {
		[Theory]
		[InlineData(true, true, "Value cannot be null.\r\nParameter name: weatherService")]
		[InlineData(true, false, "Value cannot be null.\r\nParameter name: weatherService")]
		[InlineData(false, true, "Value cannot be null.\r\nParameter name: loggerFactory")]
		public void Constructing_WhenArgumentIsNull_ThrowArgumentNullException(bool isCurrenWeatherServiceNull, bool isLoggerNull, string exceptionMessage)
		{
			ICurrentWeatherService currentWeatherService = isCurrenWeatherServiceNull ? null : TestHelper.GetCurrentWeatherServiceMock().Object;
			Microsoft.Extensions.Logging.ILoggerFactory logger = isLoggerNull ? null : TestHelper.GetLoggerFactoryMock().Object;

			ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => new CitySearchCommand(currentWeatherService, logger));

			Assert.NotNull(ex);
			Assert.Equal(exceptionMessage, ex.Message);
		}

		[Fact]
		public void ExecuteAsync_WhenCalled_ReturnSuccessful()
		{
			ICurrentWeatherService currentWeatherService = TestHelper.GetCurrentWeatherServiceMock().Object;
			Microsoft.Extensions.Logging.ILoggerFactory loggerFactory = TestHelper.GetLoggerFactoryMock().Object;

			CitySearchViewModel result = new CitySearchCommand(currentWeatherService, loggerFactory)
				.ExecuteAsync("auditId", "Guiseley")
				.Result;

			Assert.NotNull(result);
		}
	}
}
