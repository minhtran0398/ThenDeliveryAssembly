using Microsoft.AspNetCore.Components;
using ThenDelivery.Shared.Helper;

namespace ThenDelivery.Client.Components.Commons
{
	public class CustomTimeEditBase : ComponentBase
	{
		[Parameter] public CustomTime InputTime { get; set; }

		protected void HandleTimeChanged(string newTime)
		{

		}
	}
}
