namespace WeatherApp.Common
{
	public interface IConfig
    {
		string OpenWeatherAPIKey { get; set; }

		string CitySearchEndpointMask { get; set; }

		string CityIdLookupEndpointMask { get; set; }

		int CacheItemExpirationInSeconds { get; set; }
	}
}
