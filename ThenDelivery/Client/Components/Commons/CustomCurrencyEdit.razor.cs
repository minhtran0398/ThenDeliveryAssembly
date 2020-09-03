﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ThenDelivery.Client.ExtensionMethods;

namespace ThenDelivery.Client.Components.Commons
{
	public class CustomCurrencyEditBase : ComponentBase
	{
		[Inject] public ILogger<CustomCurrencyEditBase> Logger { get; set; }
      [CascadingParameter] public EditContext FormContext { get; set; }
      [Parameter] public decimal InputValue { get; set; }
		[Parameter] public EventCallback<decimal> ValueChanged { get; set; }
		[Parameter] public decimal? MinValue { get; set; }
		[Parameter] public decimal? MaxValue { get; set; }
		[Parameter] public string DisplayFormat { get; set; } = "N0";
      [Parameter] public Expression<Func<decimal>> ExpressionValue { get; set; }

      protected string ValueString { get; set; }

		protected override void OnInitialized()
		{
			ValueString = InputValue.ToString();
		}

		protected async Task HandleValueChanged(string newValueString)
		{
			// logic parse
			// set oldValue is default for newValue in case error occur
			decimal newValue = Decimal.Parse(ValueString);
			if (decimal.TryParse(newValueString, out newValue))
			{
				if (MinValue != null && newValue < MinValue)
				{
					newValue = (decimal)MinValue;
				}
				else if (MaxValue != null && newValue > MaxValue)
				{
					newValue = (decimal)MaxValue;
				}
				ValueString = newValue.ToString();
				await ValueChanged.InvokeAsync(newValue);
			}
			await InvokeAsync(StateHasChanged);
		}
	}
}
