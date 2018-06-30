using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherApp.Service
{
	[ExcludeFromCodeCoverage]
	public class HttpClientWrapper : IHttpClient
	{
		private readonly HttpClient _httpClient;

		public HttpClientWrapper(HttpClient httpClient)
		{
			this._httpClient = httpClient ?? throw new ArgumentNullException("httpClient");
		}

		public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
		{
			return await this._httpClient
				.SendAsync(request)
				.ConfigureAwait(false);
		}
	}
}
