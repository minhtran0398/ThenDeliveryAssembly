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
		public CustomTime(int hour, int minute, bool isCheckValidTime = true)
		{
			if (isCheckValidTime)
			{
				if (hour > 23)
				{
					hour = 23;
					minute = 59;
				}
				if (hour < 0)
				{
					hour = 0;
					minute = 0;
				}
				if (minute > 59) minute = 59;
				if (minute < 0) minute = 0;
			}
			_hour = hour;
			_minute = minute;
		}

		/// <summary>
		/// Parse a string to a time
		/// </summary>
		/// <example>"0600" => 06:00</example>
		/// <param name="timeString">time as a string format</param>
		/// <returns>CustomTime object</returns>
		public static CustomTime Parse(string timeString, char separator = ':')
		{
			try
			{
				if (timeString.Contains(separator))
				{
					return ParseWithSeparator(timeString, separator);
				}
				else
				{
					switch (timeString.Length)
					{
						case 1:
							return ParseWithOneNumber(timeString);
						case 2:
							return ParseWithTwoNumber(timeString);
						case 3:
							return ParseWithThreeNumber(timeString);
						case 4:
						default:
							return ParseWithFourNumber(timeString);
					}
				}
			}
			catch (InvalidCastException)
			{
				throw new InvalidCastException("Cannot convert string to custom time");
			}
		}

		public string TimeString
		{
			get
			{
				return String.Format("{0:D2}:{1:D2}", _hour, _minute);
			}
			set
			{

			}
		}

		public string ToStringWithoutDelimiter()
		{
			return String.Format("{0:D2}{1:D2}", _minute, _hour);
		}

		private static CustomTime ParseWithOneNumber(string timeString)
		{
			return new CustomTime(Int32.Parse(timeString), 0);
		}

		private static CustomTime ParseWithTwoNumber(string timeString)
		{
			int timeInt = Int32.Parse(timeString);
			if (timeInt < 24)
			{
				return new CustomTime(timeInt, 0);
			}
			else
			{
				return new CustomTime(timeInt / 10, timeInt % 10);
			}
		}

		private static CustomTime ParseWithThreeNumber(string timeString)
		{
			// case time HH:0m
			if ((Convert.ToInt32(timeString[0]) < 2)
				|| (Convert.ToInt32(timeString[0]) == 2 && Convert.ToInt32(timeString[1]) < 4))
			{
				return new CustomTime(Int32.Parse(timeString.Substring(0, 2)), Int32.Parse(timeString.Substring(2)));
			}
			// case time 0H:mm
			else
			{
				return new CustomTime(Int32.Parse(timeString.Substring(0, 1)), Int32.Parse(timeString.Substring(1)));
			}
		}

		private static CustomTime ParseWithFourNumber(string timeString)
		{
			return new CustomTime(Int32.Parse(timeString.Substring(0, 2)), Int32.Parse(timeString.Substring(2)));
		}

		private static CustomTime ParseWithSeparator(string timeString, char separator)
		{
			string[] timeStringSplited = timeString.Split(separator);
			int hour = Int32.Parse(timeStringSplited[0]);
			int minute = Int32.Parse(timeStringSplited[1]);
			return new CustomTime(hour, minute);
		}
	}
}
