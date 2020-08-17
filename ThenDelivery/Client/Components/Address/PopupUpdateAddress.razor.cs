using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Components.Address
{
	public enum PopupAddressMode
   {
		Select,
		SelectEdit,
		CreateNew
   }

	public class PopupUpdateAddressBase : CustomComponentBase<PopupUpdateAddressBase>
	{
		[Parameter] public OrderDto Order { get; set; }
		[Parameter] public List<ShippingAddressDto> ShippingAddressList { get; set; }
		[Parameter] public EventCallback OnClose { get; set; }
		[Parameter] public EventCallback OnConfirm { get; set; }

      public PopupAddressMode AddressMode { get; set; }

      protected override void OnInitialized()
      {
			AddressMode = PopupAddressMode.Select;
      }

      protected async Task HanldeEditShippingAddress(ShippingAddressDto address)
		{
			AddressMode = PopupAddressMode.SelectEdit;
			// set value => not set directly to another object
			Order.ShippingAddress = address;
			await InvokeAsync(StateHasChanged);
		}

		protected string GetStringMode()
      {
         return AddressMode switch
         {
				PopupAddressMode.Select => "Create new",
            PopupAddressMode.SelectEdit => "Create new",
            PopupAddressMode.CreateNew => "Choose your address",
            _ => "Unspecified mode",
         };
      }

		protected async Task HandleChangeShippingAddress(ShippingAddressDto shippingAddress)
      {
			Order.ShippingAddress = shippingAddress;
			await InvokeAsync(StateHasChanged);
      }

		protected async Task HandleChangeMode()
      {
         switch (AddressMode)
         {
            case PopupAddressMode.Select:
            case PopupAddressMode.SelectEdit:
					// not new model of form context => set default value
					Order.ShippingAddress = new ShippingAddressDto();
					AddressMode = PopupAddressMode.CreateNew;
               break;
            case PopupAddressMode.CreateNew:
					AddressMode = PopupAddressMode.SelectEdit;
					break;
            default:
               break;
         }
         await InvokeAsync(StateHasChanged);
      }

		protected async Task HandleChangeFullName(string newValue)
      {
			Order.ShippingAddress.FullName = newValue;
			await InvokeAsync(StateHasChanged);
      }

		protected async Task HandleChangePhoneNumber(string newValue)
		{
			Order.ShippingAddress.PhoneNumber = newValue;
			await InvokeAsync(StateHasChanged);
		}

		protected async Task HandleClose()
		{
			await OnClose.InvokeAsync(null);
		}

		protected async Task HandleConfirm()
		{
			await OnConfirm.InvokeAsync(null);
		}

		protected void HandleSelectedCityChanged(CityDto newValue)
		{
			Order.ShippingAddress.City = newValue;
		}

		protected void HandleSelectedDistrictChanged(DistrictDto newValue)
		{
			Order.ShippingAddress.District = newValue;
		}

		protected void HandleSelectedWardChanged(WardDto newValue)
		{
			Order.ShippingAddress.Ward = newValue;
		}

		protected void HandleHouseNumberChanged(string newValue)
		{
			Order.ShippingAddress.HouseNumber = newValue;
		}

		#region Methods
		protected async Task<IEnumerable<MerTypeDto>> HandleLoadMerchantTypeAsync(CancellationToken _ = default)
		{
			return await HttpClientServer.CustomGetAsync<IEnumerable<MerTypeDto>>("api/mertype");
		}

		protected async Task<IEnumerable<FeaturedDishDto>> HandleLoadFeaturedDishCategoryAsync(CancellationToken _ = default)
		{
			return await HttpClientServer.CustomGetAsync<IEnumerable<FeaturedDishDto>>("api/featureddish");
		}

		protected async Task<IEnumerable<CityDto>> HandleLoadCitiesAsync(CancellationToken _ = default)
		{
			return await HttpClientServer.CustomGetAsync<IEnumerable<CityDto>>("api/city");
		}

		protected async Task<IEnumerable<DistrictDto>> HandleLoadDistrictsAsync(CancellationToken _ = default)
		{
			return await HttpClientServer.CustomGetAsync<IEnumerable<DistrictDto>>("api/district");
		}

		protected async Task<IEnumerable<WardDto>> HandleLoadWardsAsync(CancellationToken _ = default)
		{
			return await HttpClientServer.CustomGetAsync<IEnumerable<WardDto>>("api/ward");
		}
		#endregion
	}
}
