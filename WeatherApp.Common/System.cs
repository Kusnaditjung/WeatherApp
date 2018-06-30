using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace WeatherApp.Common
{
	[ExcludeFromCodeCoverage]
	public class System
	{
		[JsonProperty("type")]
		public int Type { get; set; }

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("message")]
		public double Message { get; set; }

		[JsonProperty("country")]
		public string Country { get; set; }

		[JsonProperty("sunrise")]
		public double Sunrise { get; set; }

		[JsonProperty("sunset")]
		public double Sunset { get; set; }

		[JsonIgnore]
		public DateTime SunriseDateTime
		{
			get
			{
				return UnixTimeUtility.ToDateTime(Sunrise);
			}
		}

		[JsonIgnore]
		public DateTime SunsetDateTime
		{
			get
			{
				return UnixTimeUtility.ToDateTime(Sunset);
			}
		}
	}
}
