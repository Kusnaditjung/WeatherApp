using System;
using System.Net.Http;
using WeatherApp.Common;
using WeatherApp.Service;
using Xunit;

namespace WeatherApp.Tests.Service
{
	public class HttpResponseMessageExtensionsTest
	{
		[Fact]
		public void GetTypeResult_WhenGivenNullArgument_ThrowArgumentNullException()
		{
			ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => HttpResponseMessageExtensions.GetTypedResult<WeatherDetail>(null));
			Assert.NotNull(ex);
		}

		[Fact]
		public void GetTypeResult_WhenGivenResponseWithWeatherDetailJson_ReturnWeatherDetail()
		{
			var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
			{
				Content = new StringContent(TestHelper.GetWeatherDetailJson())
			};

			using (response)
			{
				var weatherDetail = HttpResponseMessageExtensions.GetTypedResult<WeatherDetail>(response);

				Assert.NotNull(weatherDetail);
				Assert.Equal("base", weatherDetail.Base);
			}
		}
	}
}