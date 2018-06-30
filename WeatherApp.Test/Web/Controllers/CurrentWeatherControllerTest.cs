using System;
using WeatherApp.Web.Commands;
using WeatherApp.Web.Controllers;
using Xunit;

namespace WeatherApp.Tests.Web.Controllers
{
	public class CurrentWeatherControllerTest
    {
		[Theory]
		[InlineData(true, true, "Value cannot be null.\r\nParameter name: citySearchCommand")]
		[InlineData(true, false, "Value cannot be null.\r\nParameter name: citySearchCommand")]
		[InlineData(false, true, "Value cannot be null.\r\nParameter name: uKCityCommand")]
		public void Constructing_WhenArgumentIsNull_ThrowArgumentNullException(bool isCitySearchCommandNull, bool isCityIdLookupCommandNull, string exceptionMessage)
		{
			var citySearchCommandLazy = isCitySearchCommandNull ? null : TestHelper.GetCitySearchCommandLazy(true);
			var cityLookupCommandLazy = isCityIdLookupCommandNull ? null : TestHelper.GetUKCityCommandLazy(true); 

			ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => new CurrentWeatherController(citySearchCommandLazy, cityLookupCommandLazy));

			Assert.NotNull(ex);
			Assert.Equal(exceptionMessage, ex.Message);
		}

		[Fact]
		public void CitySearch_WhenCitySearchCommandPresent_ReturnSuccessful()
		{
			var controller = new CurrentWeatherController(TestHelper.GetCitySearchCommandLazy(true), TestHelper.GetUKCityCommandLazy(false));
			var result = controller.CitySearch(string.Empty).Result;
			Assert.NotNull(result);
		}

		[Fact]
		public void CitySearch_WhenCitySearchCommandNotPresent_ThrowException()
		{
			var controller = new CurrentWeatherController(TestHelper.GetCitySearchCommandLazy(false), TestHelper.GetUKCityCommandLazy(true));
			AggregateException ex = Assert.Throws<AggregateException>(() => controller.CitySearch(string.Empty).Result);
			Assert.NotNull(ex);
		}

		[Fact]
		public void UKCity_WhenUKCityCommandPresent_ReturnSuccesful()
		{
			var controller = new CurrentWeatherController(TestHelper.GetCitySearchCommandLazy(false), TestHelper.GetUKCityCommandLazy(true));
			var result = controller.UKCity().Result;
			Assert.NotNull(result);
		}

		[Fact]
		public void UKCity_WhenUKCityCommandNotPresent_ReturnSuccesful()
		{
			var controller = new CurrentWeatherController(TestHelper.GetCitySearchCommandLazy(true), TestHelper.GetUKCityCommandLazy(false));
			AggregateException ex = Assert.Throws<AggregateException>(() => controller.UKCity().Result);
			Assert.NotNull(ex);
		}
	}
}
