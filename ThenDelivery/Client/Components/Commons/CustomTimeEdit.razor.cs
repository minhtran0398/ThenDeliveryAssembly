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
			if(newTimeString.Length <= 5 && Int32.TryParse(newTimeString, out _))
			{
				InputTime = CustomTime.Parse(newTimeString);
			}
			await TimeChanged.InvokeAsync(InputTime);
			await InvokeAsync(StateHasChanged);
		}
	}
}
