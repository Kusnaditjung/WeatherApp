using WeatherApp.Web.Models;

namespace WeatherApp.Web.Commands
{
	public interface ICitySearchCommand : IAsyncCommand<string, string, CitySearchViewModel>
    {
	}
}
