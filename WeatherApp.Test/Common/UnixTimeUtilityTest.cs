using System;
using Xunit;

namespace WeatherApp.Tests.Common
{
    public class UnixTimeUtilityTest
    {
		[Fact]
		public void ToDateTime_WhenGivenAValue_ReturnCorrectConvesion()
		{
			DateTime dateTime = WeatherApp.Common.UnixTimeUtility.ToDateTime(123456771);
			Assert.Equal("29/11/1973 21:32:51", dateTime.ToString());
		}
	}
}
