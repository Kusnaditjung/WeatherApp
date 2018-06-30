using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace WeatherApp.Common
{
	[ExcludeFromCodeCoverage]
	public class Main
	{
		[JsonProperty("temp")]
		public double Temperature { get; set; }

		[JsonProperty("pressure")]
		public double Pressure { get; set; }

		[JsonProperty("humidity")]
		public double Humidity { get; set; }

		[JsonProperty("temp_min")]
		public double MinimumTemperature { get; set; }

		[JsonProperty("temp_max")]
		public double MaximumTemperature { get; set; }

		[JsonIgnore]
		public string PressureUnit { get; } = "hPA";

		[JsonIgnore]
		public string TemperatureUnit { get; } = "C";

		[JsonIgnore]
		public string HumidityUnit { get; } = "%";
	}
}
