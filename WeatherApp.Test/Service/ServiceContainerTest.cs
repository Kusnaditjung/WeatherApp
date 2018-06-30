using WeatherApp.Service;
using Xunit;

namespace WeatherApp.Tests.Service
{
    public class ServiceContainerTest
    {
		[Fact]
		public void Constructing_WhenCompleted_ContainThreeRegistration()
		{
			var container = new ServiceContainer();
			Assert.Equal(3, container.Count);
		}
    }
}
