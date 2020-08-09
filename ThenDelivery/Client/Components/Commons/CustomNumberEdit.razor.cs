using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace ThenDelivery.Client.Components.Commons
{
	public class CustomNumberEditBase : ComponentBase
	{
		[Parameter] public int InputValue { get; set; }
		[Parameter] public int MaxValue { get; set; } = int.MaxValue;
		[Parameter] public int MinValue { get; set; } = int.MinValue;
		[Parameter] public EventCallback<int> OnChangeNumber { get; set; }

		protected string InputValueString { get; set; }

		protected override void OnInitialized()
		{
			InputValueString = InputValue.ToString();
		}

		protected async Task HandleOnChangeNumber(ChangeEventArgs args)
		{
			string newValueString = args.Value.ToString();
			if (long.TryParse(newValueString, out long newValue))
			{
				if (newValue > MaxValue)
				{
					newValue = MaxValue;
				}
				else if (newValue < MinValue)
				{
					newValue = MinValue;
				}
				InputValue = (int)newValue;
				InputValueString = InputValue.ToString();
				await OnChangeNumber.InvokeAsync(InputValue);
			}
			await InvokeAsync(StateHasChanged);
		}
	}
}
