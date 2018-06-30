using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace WeatherApp.Common
{
	[ExcludeFromCodeCoverage]
	public class Wind
	{
		[JsonProperty("speed")]
		public double Speed { get; set; }

		[JsonProperty("deg")]
		public double Degree { get; set; }

		[JsonIgnore]
		public string SpeedUnit { get; } = "m/s";
	}
}
