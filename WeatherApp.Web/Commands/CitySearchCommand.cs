using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WeatherApp.Common;
using WeatherApp.Web.Models;

namespace WeatherApp.Web.Commands
{
	public class CitySearchCommand : ICitySearchCommand
	{
		private readonly ICurrentWeatherService _weatherService;
		private readonly ILogger _logger;

		public CitySearchCommand(ICurrentWeatherService weatherService, ILoggerFactory loggerFactory)
		{
			this._weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));

			if (loggerFactory == null)
			{
				throw new ArgumentNullException(nameof(loggerFactory));
			}

			this._logger = loggerFactory.CreateLogger(CommandHelper.ApplicationAuditCategory);
		}

		public string CommandName => "City Weather Search";

		public async Task<CitySearchViewModel> ExecuteAsync(string auditId, string parameter)
		{
			async Task<CitySearchViewModel> dataRetriever() =>
				new CitySearchViewModel()
				{
					SearchKey = parameter,
					WeatherDetail = await this.GetWeatherDetailAsync(parameter)
						.ConfigureAwait(false)
				};

			return await CommandHelper
				.GetData<CitySearchViewModel>(
					auditId,
					CommandName,
					parameter,
					dataRetriever,
					this._logger)
				.ConfigureAwait(false);
		}

		private async Task<WeatherDetail> GetWeatherDetailAsync(string searchKey)
		{
			if (string.IsNullOrWhiteSpace(searchKey))
			{
				return null;
			}

			string[] cityCountry = searchKey.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);

			string city = cityCountry[0];
			string country = cityCountry.Length > 1 ? cityCountry[1] : string.Empty;

			return await this._weatherService
				.GetCurrentWeatherByCityAsync(city, country)
				.ConfigureAwait(false);
		}
	}
}
