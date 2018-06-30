using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace WeatherApp.Common
{
	[ExcludeFromCodeCoverage]
	public class Weather
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("main")]
		public string Main { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("icon")]
		public string Icon { get; set; }

		[JsonIgnore]
		public string IconUrl
		{
			get
			{
				return $"http://openweathermap.org/img/w/{Icon}.png";
			}
		} 
	}
}
