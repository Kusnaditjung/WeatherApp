using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace WeatherApp.Common
{
	[ExcludeFromCodeCoverage]
	public class Coordinate
	{
		[JsonProperty("lon")]
		public double Longitude { get; set; }

		[JsonProperty("lat")]
		public double Latitude { get; set; }
	}
}
