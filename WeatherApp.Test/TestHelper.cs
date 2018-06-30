using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using WeatherApp.Common;
using WeatherApp.Service;
using WeatherApp.Web.Commands;
using WeatherApp.Web.Models;

namespace WeatherApp.Tests
{
    public static class TestHelper
    {
		public static Mock<IConfig> GetConfigMock()
		{
			var configMock = new Moq.Mock<IConfig>();
			configMock.Setup(config => config.OpenWeatherAPIKey).Returns("key");
			configMock.Setup(config => config.CitySearchEndpointMask).Returns("http://api.openweathermap.org/data/2.5/weather?q={0}{1}&units=metric&APPID={2}");
			configMock.Setup(config => config.CityIdLookupEndpointMask).Returns("http://api.openweathermap.org/data/2.5/weather?id={0}&units=metric&APPID={1}");
			configMock.Setup(config => config.CacheItemExpirationInSeconds).Returns(60);

			return configMock;
		}

		public static Mock<IHttpClient> GetHttpClientMock()
		{
			var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
			{
				Content = new StringContent(TestHelper.GetWeatherDetailJson())
			};

			var httpClientMock = new Mock<IHttpClient>();
			httpClientMock
				.Setup(httpClient => httpClient.SendAsync(It.IsAny<HttpRequestMessage>()))
				.Returns(Task.FromResult(response));

			return httpClientMock;
		}

		public static Mock<ICurrentWeatherService> GetCurrentWeatherServiceMock()
		{
			var currentWeatherServiceMock = new Moq.Mock<ICurrentWeatherService>();

			currentWeatherServiceMock
				.Setup(service => service.GetCurrentWeatherByCityAsync(It.IsAny<string>(), It.IsAny<string>()))
				.Returns(Task.FromResult(TestHelper.GetWeatherDetail()));

			currentWeatherServiceMock
				.Setup(service => service.GetCurrentWeatherByCityIdAsync(It.IsAny<int>()))
				.Returns(Task.FromResult(TestHelper.GetWeatherDetail()));

			return currentWeatherServiceMock;
		}

		public static Mock<ILoggerFactory> GetLoggerFactoryMock()
		{
			var loggerFactoryMock = new Mock<ILoggerFactory>();
			loggerFactoryMock
				.Setup(factory => factory.CreateLogger(It.IsAny<string>()))
				.Returns(new Moq.Mock<ILogger>().Object);

			return loggerFactoryMock;
		}

		public static Mock<IUKCityCommand> GetUKCityCommandMock()
		{
			var commandMock = new Mock<IUKCityCommand>();
			commandMock
				.Setup(cmd => cmd.ExecuteAsync(It.IsAny<string>()))
				.Returns(Task.FromResult(new UKCityViewModel()));

			return commandMock;
		}

		public static Lazy<ICitySearchCommand> GetCitySearchCommandLazy(bool haveContent)
		{
			if (!haveContent)
			{
				return new Lazy<ICitySearchCommand>();
			}
			else
			{
				return new Lazy<ICitySearchCommand>(() => TestHelper.GetCitySearchCommandMock().Object);
			}
		}

		public static Lazy<IUKCityCommand> GetUKCityCommandLazy(bool haveContent)
		{
			if (!haveContent)
			{
				return new Lazy<IUKCityCommand>();
			}
			else
			{
				return new Lazy<IUKCityCommand>(() => TestHelper.GetUKCityCommandMock().Object);
			}
		}

		public static IMemoryCache GetMemoryCache()
		{
			return new MemoryCache(new MemoryCacheOptions());
		}

		public static Mock<ICitySearchCommand> GetCitySearchCommandMock()
		{
			var commandMock = new Mock<ICitySearchCommand>();
			commandMock
				.Setup(cmd => cmd.ExecuteAsync(It.IsAny<string>(), It.IsAny<string>()))
				.Returns(Task.FromResult(new CitySearchViewModel()));

			return commandMock;
		}

		public static string GetWeatherDetailJson()
		{
			return Newtonsoft.Json.JsonConvert.SerializeObject(TestHelper.GetWeatherDetail());
		}

		public static IEnumerable<WeatherDetail> GetWeatherDetailCollection()
		{
			return new List<WeatherDetail>
			{
				new WeatherDetail()
				{
					Base = "base",
					Id = 123,
					Code = 200,
					Name = "Guiseley",
					System = new WeatherApp.Common.System()
					{
						Id = 10,
						Country = "GB"
					}
				},
				new WeatherDetail()
				{
					Base = "base",
					Id = 345,
					Code = 200,
					Name = "Leeds",
					System = new WeatherApp.Common.System()
					{
						Id = 10,
						Country = "GB"
					}
				}
			};
		}

		public static WeatherDetail GetWeatherDetail()
		{
			return new WeatherDetail()
			{
				Base = "base",
				Id = 123,
				Code = 200,
				System = new WeatherApp.Common.System()
				{
					Id = 10,
					Country = "GB"
				}
			};
		}
	}
}
