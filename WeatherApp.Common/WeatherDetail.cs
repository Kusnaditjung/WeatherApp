using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace WeatherApp.Common
{
	[ExcludeFromCodeCoverage]
	public class WeatherDetail
	{
		[JsonProperty("coord")]
		public Coordinate Coordinate { get; set; }

		[JsonProperty("weather")]
		public List<Weather> Weather { get; set; }

		[JsonProperty("base")]
		public string Base { get; set; }

		[JsonProperty("main")]
		public Main Main { get; set; }

		[JsonProperty("visibility")]
		public int Visibility { get; set; }

		[JsonProperty("wind")]
		public Wind Wind { get; set; }

		[JsonProperty("clouds")]
		public Cloud Cloud { get; set; }

		[JsonProperty("dt")]
		public int LastUpdate { get; set; }		

		[JsonProperty("sys")]
		public System System { get; set; }

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("cod")]
		public int Code { get; set; }

		[JsonIgnore]
		public DateTime LastUpdateDateTime
		{
			get
			{
				return UnixTimeUtility.ToDateTime(this.LastUpdate);
			}
		}
	}
}
