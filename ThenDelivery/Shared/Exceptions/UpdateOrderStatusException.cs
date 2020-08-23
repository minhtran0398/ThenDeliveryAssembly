using System;

namespace ThenDelivery.Shared.Exceptions
{
	public class UpdateOrderStatusException : Exception
	{
		public int StatusCode
		{
			get
			{
				return 401;
			}
		}

		public UpdateOrderStatusException()
			: base("Order already in this status")
		{

		}
	}
}