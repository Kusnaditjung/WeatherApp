using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WeatherApp.Common;
using WeatherApp.Web.Models;

namespace WeatherApp.Web.Commands
{
	public class UKCityCommand : IUKCityCommand
	{
		private readonly ICurrentWeatherService _weatherService;
		private readonly ILogger _logger;

		private readonly IEnumerable<int> _ukCityIds = new List<int>
		{
			2643743, //London			
			2643123, //Manchester,
			3333229, //Edinburgh,
			2648579, //Glasgow
			2652355, //Cornwall
			2655603, //Birmingham
			2655984, //Belfast
			2653822, //Cardiff
			2644688 //Leeds
		};

		public UKCityCommand(ICurrentWeatherService weatherService, ILoggerFactory loggerFactory)
		{
			this._weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));

			if (loggerFactory == null)
			{
				throw new ArgumentNullException(nameof(loggerFactory));
			}

			this._logger = loggerFactory.CreateLogger(CommandHelper.ApplicationAuditCategory);
		}

		public string CommandName => "UK City Weather";

		public async Task<UKCityViewModel> ExecuteAsync(string auditId)
		{
			async Task<UKCityViewModel> dataRetriever() =>
				new UKCityViewModel()
				{
					WeatherDetails = await Task
						.WhenAll(this._ukCityIds
							.Select(cityId => this._weatherService
								.GetCurrentWeatherByCityIdAsync(cityId)))
						.ConfigureAwait(false)
				};

			return await CommandHelper
				.GetData<UKCityViewModel>(
					auditId,
					"UK Cities",
					string.Empty,
					dataRetriever,
					this._logger)
				.ConfigureAwait(false);
		}
	}
}
