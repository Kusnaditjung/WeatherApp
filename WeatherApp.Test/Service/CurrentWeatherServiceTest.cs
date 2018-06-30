using System;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using WeatherApp.Common;
using WeatherApp.Service;
using Xunit;

namespace WeatherApp.Tests.Service
{
	public class CurrentWeatherServiceTest
    {
		[Theory]
		[InlineData(true,true, false, "Value cannot be null.\r\nParameter name: httpClient")]
		[InlineData(true, false, false, "Value cannot be null.\r\nParameter name: httpClient")]
		[InlineData(false, true, false, "Value cannot be null.\r\nParameter name: config")]
		[InlineData(false, false, true, "Value cannot be null.\r\nParameter name: memoryCache")]
		public void Constructing_WhenHttpClientIsNull_ThrowArgumentNullException(bool isHttpNull, bool isConfigNull, bool isMemoryCacheNull, string exceptionMessage)
		{
			IHttpClient httpClient = isHttpNull ? null : TestHelper.GetHttpClientMock().Object;
			IConfig config = isConfigNull ? null : TestHelper.GetConfigMock().Object;
			IMemoryCache memoryCache = isMemoryCacheNull ? null : TestHelper.GetMemoryCache();

			ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => new CurrentWeatherService(httpClient, config, memoryCache));

			Assert.NotNull(ex);
			Assert.Equal(exceptionMessage, ex.Message);
		}

		[Fact]
		public void GetCurrentWeatherByCityAsync_WhenGivenSearchKey_ReturnCorrectResult()
		{
			Mock<IHttpClient> httpClientMock = TestHelper.GetHttpClientMock();
			Mock<IConfig> configMock = TestHelper.GetConfigMock();

			var currentWeatherService = new CurrentWeatherService(httpClientMock.Object, configMock.Object, TestHelper.GetMemoryCache());
			WeatherDetail weatherDetail = currentWeatherService.GetCurrentWeatherByCityAsync("Guiseley", "UK").Result;

			Assert.NotNull(weatherDetail);
		}

		[Fact]
		public void GetCurrentWeatherByCityIdAsync_WhenGivenCityId_ReturnCorrectResult()
		{
			Mock<IHttpClient> httpClientMock = TestHelper.GetHttpClientMock();
			Mock<IConfig> configMock = TestHelper.GetConfigMock();

			var currentWeatherService = new CurrentWeatherService(httpClientMock.Object, configMock.Object, TestHelper.GetMemoryCache());
			WeatherDetail weatherDetail = currentWeatherService.GetCurrentWeatherByCityIdAsync(123).Result;

			Assert.NotNull(weatherDetail);
		}
	}
}
