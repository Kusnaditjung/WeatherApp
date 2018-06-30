using Microsoft.Extensions.Caching.Memory;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Common;

namespace WeatherApp.Service
{
	public class CurrentWeatherService : ICurrentWeatherService
	{
		private readonly IConfig _config;
		private readonly IHttpClient _httpClient;
		private readonly IMemoryCache _memoryCache;
		private readonly TimeSpan _cacheItemTimeSpan;

		public CurrentWeatherService(IHttpClient httpClient, IConfig config, IMemoryCache memoryCache)
		{
			this._httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
			this._config = config ?? throw new ArgumentNullException(nameof(config));
			this._memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));

			this._cacheItemTimeSpan = new TimeSpan(0, 0, 0, this._config.CacheItemExpirationInSeconds);
		}

		public async Task<WeatherDetail> GetCurrentWeatherByCityAsync(string city, string country)
		{
			Func<string> citySearchUriDelegate = () => this.GetUriForCitySearch(city, country);

			string cacheKey = $"{city}{country}";

			WeatherDetail weatherDetail;
			if (!this._memoryCache.TryGetValue<WeatherDetail>(cacheKey, out weatherDetail))
			{
				weatherDetail = await this.GetWeatherDetail(citySearchUriDelegate)
					.ConfigureAwait(false);

				this._memoryCache.Set<WeatherDetail>(cacheKey, weatherDetail, this._cacheItemTimeSpan);
			}

			return weatherDetail;
		}

		public async Task<WeatherDetail> GetCurrentWeatherByCityIdAsync(int cityId)
		{
			string cityIdLookupDelegate() => this.GetUriForCityIdLookup(cityId);

			WeatherDetail weatherDetail;

			if (!this._memoryCache.TryGetValue<WeatherDetail>(cityId, out weatherDetail))
			{
				weatherDetail = await this.GetWeatherDetail(cityIdLookupDelegate)
					.ConfigureAwait(false);

				this._memoryCache.Set<WeatherDetail>(cityId, weatherDetail, this._cacheItemTimeSpan);
			}

			return weatherDetail;
		}

		public async Task<WeatherDetail> GetWeatherDetail(Func<string> uriDelegate)
		{
			using (HttpResponseMessage response = await this.CreateResponse(uriDelegate).ConfigureAwait(false))
			{
				return response
					.GetTypedResult<WeatherDetail>();
			}
		}

		private string GetUriForCitySearch(string city, string country)
		{
			if (string.IsNullOrWhiteSpace(city))
			{
				throw new ArgumentException("city is null or whitespace");
			}

			string countryPart = string.IsNullOrWhiteSpace(country) ? string.Empty : "," + country;

			return string.Format(
				this._config.CitySearchEndpointMask,
				city,
				countryPart,
				this._config.OpenWeatherAPIKey);
		}

		private string GetUriForCityIdLookup(int cityId)
		{
			return string.Format(
				this._config.CityIdLookupEndpointMask,
				cityId.ToString(),
				this._config.OpenWeatherAPIKey);
		}

		private async Task<HttpResponseMessage> CreateResponse(Func<string> uriDelegate)
		{
			using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(uriDelegate())))
			{
				return await this._httpClient
					.SendAsync(request)
					.ConfigureAwait(false);
			}
		}
	}
}
