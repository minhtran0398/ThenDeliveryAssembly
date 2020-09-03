using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Shared.Validation;

namespace ThenDelivery.Client.Components.Topping
{
	public class AddToppingBase : CustomComponentBase<AddToppingBase>, IDisposable
	{
      public EditContext FormContext { get; set; }
      [Parameter] public List<ToppingDto> ToppingListModel { get; set; }
		[Parameter] public EventCallback OnSubmit { get; set; }
		[Parameter] public EventCallback OnCancel { get; set; }

		public List<ToppingDto> ToppingList { get; set; }

		protected override void OnInitialized()
		{
			base.OnInitialized();
			ToppingList = new List<ToppingDto>();
			if(ToppingListModel != null)
         {
            foreach (var topping in ToppingListModel)
            {
					ToppingList.Add(new ToppingDto(topping));
            }
         }
			if (ToppingList.Count == 0)
			{
				ToppingList.Add(new ToppingDto() { Id = 1 });
			}
			FormContext = new EditContext(new Temp() { ToppingDtoList = ToppingList });
		}

		protected bool IsEnableSubmit()
      {
			return FormContext.Validate();
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

		protected async Task HandleOnCancel()
		{
			await OnCancel.InvokeAsync(null);
			Dispose();
		}

		protected async Task HandleOnSubmit()
		{
			if (FormContext.Validate())
			{
				if(ToppingListModel is null)
            {
					ToppingListModel = new List<ToppingDto>();
				}
            else
            {
					ToppingListModel.Clear();
            }
            foreach (var topping in ToppingList)
            {
					ToppingListModel.Add(new ToppingDto(topping));
            }
				await OnSubmit.InvokeAsync(null);
				Dispose();
			}
		}

      public void Dispose()
      {
			GC.SuppressFinalize(this);
		}
   }
}
