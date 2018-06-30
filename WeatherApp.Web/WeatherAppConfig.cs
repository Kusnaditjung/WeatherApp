using System.IO;
using Microsoft.Extensions.Configuration;
using WeatherApp.Common;

namespace WeatherApp.Web
{
	public class WeatherAppConfig : IConfig
	{
		private const string _configFile = "appsettings.json";
		private const string _configSectionKey = "WeatherApp";

		public WeatherAppConfig()
		{
			new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile(_configFile)
				.Build()
				.GetSection(_configSectionKey)
				.Bind(this);
		}

		public string OpenWeatherAPIKey { get; set; }

		public string CitySearchEndpointMask { get; set; }

		public string CityIdLookupEndpointMask { get; set; }

		public int CacheItemExpirationInSeconds { get; set; }
	}
}
