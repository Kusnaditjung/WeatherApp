using WeatherApp.Web.Models;

namespace WeatherApp.Web.Commands
{
    public interface IUKCityCommand : IAsyncCommand<string, UKCityViewModel>
    {
	}
}
