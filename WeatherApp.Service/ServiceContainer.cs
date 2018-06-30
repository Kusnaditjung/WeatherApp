using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using WeatherApp.Common;

namespace WeatherApp.Service
{
	public class ServiceContainer : ServiceCollection
	{
		public ServiceContainer()
			: base()
		{
			this
				.AddSingleton<HttpClient>()
				.AddSingleton<IHttpClient, HttpClientWrapper>()
				.AddSingleton<ICurrentWeatherService, CurrentWeatherService>();
		}
	}
}
