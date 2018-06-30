using System.Diagnostics.CodeAnalysis;

namespace WeatherApp.Web.Models
{
	[ExcludeFromCodeCoverage]
	public class CitySearchViewModel
	{
		public string SearchKey { get; set; }

		public Common.WeatherDetail WeatherDetail { get; set; }
    }
}
