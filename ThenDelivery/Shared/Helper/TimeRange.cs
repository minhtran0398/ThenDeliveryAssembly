using System;

namespace ThenDelivery.Shared.Helper
{
	public class TimeRange
	{
		public static int DaysBetween(DateTime time1, DateTime time2)
		{
			TimeSpan timeBetween = time1 - time2;
			return Math.Abs(timeBetween.Days);
		}
	}
}