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

		public static string GetDurationString(DateTime largeTime, DateTime smallTime)
      {
			TimeSpan duration = largeTime - smallTime;
			if(duration.TotalSeconds < 60)
         {
				return string.Format("{0:ss} giây trước", duration);
         }
			if(duration.TotalMinutes < 60)
         {
				return string.Format("{0:mm} phút trước", duration);
         }
			return string.Format("{0} giờ trước", Math.Truncate(duration.TotalHours));
		}

		public static string GetDurationStringToNow(DateTime time)
		{
			return GetDurationString(DateTime.Now, time);
		}
	}
}