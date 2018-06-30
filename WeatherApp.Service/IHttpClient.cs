using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherApp.Service
{
	public interface IHttpClient
    {
		Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
	}
}
