using System;

namespace WeatherApp.Common
{
	public class UnixTimeUtility
    {
		public static DateTime ToDateTime(double unixTimeStamp)
		{
			return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
				.AddSeconds(unixTimeStamp)
				.ToLocalTime();
		}
	}
}
