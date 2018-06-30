using System;
using System.Net.Http;
using Newtonsoft.Json;

namespace WeatherApp.Service
{
	public static class HttpResponseMessageExtensions
	{
		public static T GetTypedResult<T>(this HttpResponseMessage response) 
			where T : class
		{
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}

			return JsonConvert.DeserializeObject<T>(response
				.Content
				.ReadAsStringAsync()
				.Result);
		}
	}
}
