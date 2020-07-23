using System;

namespace ThenDelivery.Shared.Helper
{
	public class CustomTime
	{
		private int _minute;
		private int _hour;

		public int Minute
		{
			get { return _minute; }
			set { _minute = value; }
		}
		public int Hour
		{
			get { return _hour; }
			set { _hour = value; }
		}

		public CustomTime()
		{
			_minute = 0;
			_hour = 0;
		}
		public CustomTime(int minute, int hour)
		{
			_minute = minute;
			_hour = hour;
		}

		public string TimeString
		{
			get
			{
				return String.Format("{0:D2}:{1:D2}", _minute, _hour);
			}
			set
			{

			}
		}

		public string ToStringWithoutDelimiter()
		{
			return String.Format("{0:D2}{1:D2}", _minute, _hour);
		}
	}
}
