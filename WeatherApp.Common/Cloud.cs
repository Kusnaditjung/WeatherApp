using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WeatherApp.Common
{
	[ExcludeFromCodeCoverage]
    public class Cloud
    {
        [JsonProperty("all")]
        public double All { get; set; }

        [JsonIgnore]
        public string Unit { get; } = "%";
    }
}
