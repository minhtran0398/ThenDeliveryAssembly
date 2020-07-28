using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace ThenDelivery.Client.Components.Commons
{
	public class CustomCurrencyEditBase : ComponentBase
	{
		[Parameter] public decimal InputCurrency { get; set; }
		[Parameter] public EventCallback<decimal> CurrencyChanged { get; set; }

		protected string StringValue { get; set; }

		protected async Task HandleCurrencyChanged(string newCurrencyString)
		{
			// logic parse
			decimal newValue = 0M;
			await CurrencyChanged.InvokeAsync(newValue);
		}
	}
}
