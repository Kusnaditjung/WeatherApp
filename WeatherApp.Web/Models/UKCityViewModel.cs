using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace WeatherApp.Web.Models
{
	[ExcludeFromCodeCoverage]
	public class UKCityViewModel
    {
		public IEnumerable<Common.WeatherDetail> WeatherDetails { get; set; }
    }
}
