using System;
using System.Threading.Tasks;

namespace WeatherApp.Common
{
    public interface ICurrentWeatherService
    {
		Task<WeatherDetail> GetCurrentWeatherByCityAsync(string city, string country);

		Task<WeatherDetail> GetCurrentWeatherByCityIdAsync(int cityId);
	}
}
