using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using ThenDelivery.Shared.Helper;

namespace ThenDelivery.Client.Components.Commons
{
	public class CustomTimeEditBase : ComponentBase
	{
		[Parameter] public CustomTime InputTime { get; set; }
		[Parameter] public EventCallback<CustomTime> TimeChanged { get; set; }

		protected async Task HandleTimeChanged(string newTimeString)
		{
			if(string.IsNullOrWhiteSpace(newTimeString) == false)
         {
				try
				{
					InputTime.TimeString = newTimeString;
					await TimeChanged.InvokeAsync(InputTime);
				}
				catch (InvalidCastException)
				{

				}
			}
			StateHasChanged();
			await InvokeAsync(StateHasChanged);
		}
	}
}
