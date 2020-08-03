namespace ThenDelivery.Shared.Helper.ExtensionMethods
{
	public static class CustomTimeExtension
	{
		public static bool IsInRange(this CustomTime time, CustomTime minTime, CustomTime maxTime)
		{
			if (time >= minTime && time <= maxTime)
				return true;
			return false;
		}
	}
}
