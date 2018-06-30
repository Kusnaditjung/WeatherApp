using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Web.Commands;
using WeatherApp.Web.Models;

namespace WeatherApp.Web.Controllers
{
	public class CurrentWeatherController : Controller
    {
		private readonly Lazy<ICitySearchCommand> _citySearchCommand;
		private readonly Lazy<IUKCityCommand> _uKCityCommand;

		public CurrentWeatherController(Lazy<ICitySearchCommand> citySearchCommand, Lazy<IUKCityCommand> uKCityCommand)
		{
			this._citySearchCommand = citySearchCommand ?? throw new ArgumentNullException(nameof(citySearchCommand));
			this._uKCityCommand = uKCityCommand ?? throw new ArgumentNullException(nameof(uKCityCommand));
		}

		public async Task<IActionResult> CitySearch(string searchKey)
		{
			CitySearchViewModel citySearchViewModel = await this._citySearchCommand
				.Value
				.ExecuteAsync(this.GetAuditId(), searchKey)
				.ConfigureAwait(false);

			return this.View(citySearchViewModel);
		}

		public async Task<IActionResult> UKCity()
		{
			UKCityViewModel citiesViewModel = await this._uKCityCommand
				.Value
				.ExecuteAsync(this.GetAuditId())
				.ConfigureAwait(false);

			return this.View(citiesViewModel);
		}

		private string GetAuditId()
		{
			return this.HttpContext?.TraceIdentifier ?? Guid.NewGuid().ToString();
		}
	}
}
