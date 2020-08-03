using System;

namespace ThenDelivery.Shared.Helper.ExtensionMethods
{
	public static class DateTimeExtension
	{
		public static bool IsInRange(this DateTime date, DateTime minDate, DateTime maxDate)
		{
			if (date >= minDate && date <= maxDate)
			{
				return true;
			}
			return false;
		}
	}
}
