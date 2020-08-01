using System;

namespace ThenDelivery.Shared.Helper
{
	public class CustomTime
	{
		private short _minute;
		private short _hour;

		public short Minute
		{
			get { return _minute; }
			set
			{
				if (value > 59) _minute = 59;
				else _minute = value;
			}
		}
		public short Hour
		{
			get { return _hour; }
			set
			{
				if (value > 23)
				{
					_hour = 23;
					_minute = 59;
				}
				else _hour = value;
			}
		}

		public CustomTime()
		{
			_minute = 0;
			_hour = 0;
		}
		public CustomTime(short hour, short minute, bool isCheckValidTime = true)
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
				if (value.Length <= 4 && Int16.TryParse(value, out _))
				{
					switch (value.Length)
					{
						case 1:
							value = String.Format("0{0}:00", value);
							break;
						case 2:
							value = String.Format("0{0}:0{1}", value[0], value[1]);
							break;
						case 3:
							value = value.Insert(0, "0").Insert(2, ":");
							break;
						case 4:
							value = value.Insert(2, ":");
							break;
					}
					string[] splitedTimeString = value.Split(':');
					Hour = Int16.Parse(splitedTimeString[0]);
					Minute = Int16.Parse(splitedTimeString[1]);
				}
				else
				{
					//throw new InvalidCastException("Cannot convert string to custom time");
				}
			}
		}

		public string ToStringWithoutDelimiter()
		{
			return String.Format("{0:D2}{1:D2}", _minute, _hour);
		}

		private static CustomTime ParseWithOneNumber(string timeString)
		{
			return new CustomTime(Int16.Parse(timeString), 0);
		}

		private static CustomTime ParseWithTwoNumber(string timeString)
		{
			short timeValue = Int16.Parse(timeString);
			if (timeValue < 24)
			{
				return new CustomTime(timeValue, 0);
			}
			else
			{
				return new CustomTime((short)(timeValue / 10), (short)(timeValue % 10));
			}
		}

		private static CustomTime ParseWithThreeNumber(string timeString)
		{
			// case time HH:0m
			if ((Convert.ToInt16(timeString[0]) < 2)
				|| (Convert.ToInt16(timeString[0]) == 2 && Convert.ToInt16(timeString[1]) < 4))
			{
				return new CustomTime(Int16.Parse(timeString.Substring(0, 2)), Int16.Parse(timeString.Substring(2)));
			}
			// case time 0H:mm
			else
			{
				return new CustomTime(Int16.Parse(timeString.Substring(0, 1)), Int16.Parse(timeString.Substring(1)));
			}
		}

		private static CustomTime ParseWithFourNumber(string timeString)
		{
			return new CustomTime(Int16.Parse(timeString.Substring(0, 2)), Int16.Parse(timeString.Substring(2)));
		}

		private static CustomTime ParseWithSeparator(string timeString, char separator)
		{
			string[] timeStringSplited = timeString.Split(separator);
			short hour = Int16.Parse(timeStringSplited[0]);
			short minute = Int16.Parse(timeStringSplited[1]);
			return new CustomTime(hour, minute);
		}
	}
}
