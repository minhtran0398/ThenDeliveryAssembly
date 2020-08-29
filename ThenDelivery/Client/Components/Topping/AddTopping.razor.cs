using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Components.Topping
{
	public class AddToppingBase : CustomComponentBase<AddToppingBase>
	{
      [CascadingParameter] public EditContext FormContext { get; set; }
      [Parameter] public List<ToppingDto> ToppingList { get; set; }
		[Parameter] public EventCallback<List<ToppingDto>> OnSubmit { get; set; }
		[Parameter] public EventCallback OnCancel { get; set; }

		protected override void OnInitialized()
		{
			base.OnInitialized();
			ToppingList ??= new List<ToppingDto>();
			if (ToppingList.Count == 0)
			{
				ToppingList.Add(new ToppingDto() { Id = 1 });
			}
		}

		protected void HandleAddTopping()
		{
			int newId = ToppingList?.Max(e => e?.Id) + 1 ?? 0;
			ToppingList.Add(new ToppingDto() { Id = newId, IsCreateNew = true });
		}

		protected async Task HandleToppingNameChanged(string newValue, int id)
		{
			ToppingList.SingleOrDefault(e => e.Id == id).Name = newValue;
			await InvokeAsync(StateHasChanged);
		}

		protected async Task HandlePriceChanged(decimal newValue, int id)
		{
			ToppingList.SingleOrDefault(e => e.Id == id).UnitPrice = newValue;
			await InvokeAsync(StateHasChanged);
		}

		protected async Task HandleDeleteTopping(int id)
		{
			if(FormContext.Validate())
         {
				var tp = ToppingList.SingleOrDefault(e => e.Id == id);
				if(tp.IsCreateNew)
            {
					ToppingList.RemoveFirst(e => e.Id == id);
            }
            else
            {
					tp.IsDelete = true;
            }
				await InvokeAsync(StateHasChanged);
			}
		}

		protected async Task HandleOnCancel()
		{
			await OnCancel.InvokeAsync(null);
		}

		protected async Task HandleOnSubmit()
		{
			await OnSubmit.InvokeAsync(ToppingList);
		}
	}
}
