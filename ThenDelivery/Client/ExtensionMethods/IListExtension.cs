using System;
using System.Collections.Generic;

namespace ThenDelivery.Client.ExtensionMethods
{
	public static class IListExtension
	{
		public static bool RemoveFirst<T>(this IList<T> list, Predicate<T> predicate)
			where T : class
		{
			for (int index = 0; index < list.Count; index++)
			{
				if (predicate(list[index]))
				{
					list.RemoveAt(index);
					return true;
				}
			}

			return false;
		}
	}
}
