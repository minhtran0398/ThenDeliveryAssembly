using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Shared.Helper;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;

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
		public List<CityDto> CityList { get; set; }
		public List<DistrictDto> DistrictList { get; set; }
		public List<DistrictDto> CurrentDistrictList { get; set; }
		public List<WardDto> WardList { get; set; }
		public List<WardDto> CurrentWardList { get; set; }
		public ShippingAddressDto ShippingAddress { get; set; }
		public EditContext FormContext { get; set; }
		public DateTime DeliveryDate { get; set; }
		public bool IsShowForm { get; set; }

		public PopupAddressMode AddressMode { get; set; }
		public CustomTime DeliveryTime { get; set; }
      public string ErrorMessage { get; set; }

      protected override void OnInitialized()
		{
			base.OnInitialized();
			ErrorMessage = string.Empty;
			DeliveryTime = new CustomTime(Order.DeliveryDateTime);
			DeliveryDate = Order.DeliveryDateTime.Date;
			AddressMode = PopupAddressMode.Select;
			if (Order.ShippingAddress == null)
			{
				ShippingAddress = new ShippingAddressDto();
			}
			else
			{
				ShippingAddress = new ShippingAddressDto(Order.ShippingAddress);
			}
			FormContext = new EditContext(ShippingAddress);
			StateHasChanged();
		}

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();

			CityList = await HttpClientServer.CustomGetAsync<List<CityDto>>("api/city");
			DistrictList = await HttpClientServer.CustomGetAsync<List<DistrictDto>>("api/district");
			WardList = await HttpClientServer.CustomGetAsync<List<WardDto>>("api/ward");

			if (CityList != null && DistrictList != null && WardList != null)
			{
				ShippingAddress.City = CityList[0];
				CurrentDistrictList = DistrictList.FindAll(e => e.CityCode == ShippingAddress.City.CityCode);
				ShippingAddress.District = CurrentDistrictList[0];
				CurrentWardList = WardList.FindAll(e => e.DistrictCode == ShippingAddress.District.DistrictCode);
				ShippingAddress.Ward = CurrentWardList[0];
			}
			await InvokeAsync(StateHasChanged);
		}

		protected override void OnAfterRender(bool firstRender)
      {
			ErrorMessage = string.Empty;
			Logger.LogInformation("OnAfterRender");
			//if (CityList != null && DistrictList != null && WardList != null)
			//{
			//	ShippingAddress.City = CityList[0];
			//	CurrentDistrictList = DistrictList.FindAll(e => e.CityCode == ShippingAddress.City.CityCode);
			//	ShippingAddress.District = CurrentDistrictList[0];
			//	CurrentWardList = WardList.FindAll(e => e.DistrictCode == ShippingAddress.District.DistrictCode);
			//	ShippingAddress.Ward = CurrentWardList[0];
			//}
		}

      protected async Task HanldeEditShippingAddress(ShippingAddressDto address)
		{
			AddressMode = PopupAddressMode.SelectEdit;
			// set value => not set directly to another object
			IsShowForm = true;
			ShippingAddress.SetData(address);
			await InvokeAsync(StateHasChanged);
		}

		protected void HandleDeliveryTimeChanged(string newTimeString)
		{
			if (string.IsNullOrWhiteSpace(newTimeString) == false)
			{
				DeliveryTime.TimeString = newTimeString;
			}
			StateHasChanged();
		}

		protected string GetStringMode()
		{
			return AddressMode switch
			{
				PopupAddressMode.Select => "Tạo mới",
				PopupAddressMode.SelectEdit => "Tạo mới",
				PopupAddressMode.CreateNew => "Chọn địa chỉ",
				_ => "Unspecified mode",
			};
		}

		protected async Task HandleChangeShippingAddress(ShippingAddressDto shippingAddress)
		{
			ShippingAddress.SetData(shippingAddress);
			await InvokeAsync(StateHasChanged);
		}

		protected async Task HandleChangeMode()
		{
			switch (AddressMode)
			{
				case PopupAddressMode.Select:
				case PopupAddressMode.SelectEdit:
					// not new model of form context => set default value
					IsShowForm = true;
					ShippingAddress.SetData(new ShippingAddressDto());
					var max = ShippingAddressList?.Max(e => e?.Id) ?? 0;
					ShippingAddress.Id = max + 1;
					FormContext = new EditContext(ShippingAddress);
					AddressMode = PopupAddressMode.CreateNew;
					break;
				case PopupAddressMode.CreateNew:
					ShippingAddress.City = CityList[0];
					IsShowForm = false;
					AddressMode = PopupAddressMode.SelectEdit;
					break;
				default:
					break;
			}
			StateHasChanged();
			await InvokeAsync(StateHasChanged);
		}

		protected async Task HandleChangeFullName(string newValue)
		{
			ShippingAddress.FullName = newValue;
			await InvokeAsync(StateHasChanged);
		}

		protected async Task HandleChangePhoneNumber(string newValue)
		{
			ShippingAddress.PhoneNumber = newValue;
			await InvokeAsync(StateHasChanged);
		}

		protected async Task HandleClose()
		{
			ShippingAddress = null;
			await OnClose.InvokeAsync(null);
		}

		protected bool IsEnableSubmit()
		{
			TimeSpan time = DeliveryTime.ToTimeSpan();
			var datetime = DeliveryDate.Date + time;
			if (datetime < DateTime.Now - TimeSpan.FromMinutes(1))
			{
				return false;
			}
			return FormContext.Validate();
		}

		protected void HandleDeliveryDateChanged(DateTime dateTime)
		{
			DeliveryDate = dateTime.Date;
			StateHasChanged();
		}

		protected async Task HandleConfirm()
		{
			Logger.LogInformation("HandleConfirm");
			ErrorMessage = string.Empty;
			if (FormContext.Validate())
			{
				// check shipping address is in another order
				bool canEdit = true;
				HttpResponseMessage response =
					await HttpClientServer.GetAsync($"api/order/can-edit-shippingAddress?shippingaddress={ShippingAddress.Id}");
				if (response.IsSuccessStatusCode)
				{
					canEdit = await response.Content.ReadFromJsonAsync<bool>();
				}
				if(canEdit == false)
            {
					ErrorMessage = "Địa chỉ này đang nằm trong 1 đơn hàng chưa hoàn thành.";
					return;
				}

				TimeSpan time = DeliveryTime.ToTimeSpan();
				Order.DeliveryDateTime = DeliveryDate.Date + time;
				if (Order.ShippingAddress is null)
				{
					Order.ShippingAddress = new ShippingAddressDto(ShippingAddress);
				}
				else
				{
					Order.ShippingAddress.SetData(ShippingAddress);
				}

				if (AddressMode == PopupAddressMode.CreateNew)
				{
					ShippingAddressList.Add(new ShippingAddressDto(ShippingAddress));
				}
				else
				{
					Logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(ShippingAddressList));
					var s = ShippingAddressList.SingleOrDefault(e => e.Id == ShippingAddress.Id);
					s.SetData(ShippingAddress);
				}
				await OnConfirm.InvokeAsync(null);
			}
		}

		protected void HandleSelectedCityChanged(CityDto newValue)
		{
			ShippingAddress.City = newValue;
			CurrentDistrictList = DistrictList.FindAll(e => e.CityCode == ShippingAddress.City.CityCode);
			ShippingAddress.District = CurrentDistrictList[0];
			CurrentWardList = WardList.FindAll(e => e.DistrictCode == ShippingAddress.District.DistrictCode);
			ShippingAddress.Ward = CurrentWardList[0];
			StateHasChanged();
		}

		protected void HandleSelectedDistrictChanged(DistrictDto newValue)
		{
			ShippingAddress.District = newValue;
			CurrentWardList = WardList.FindAll(e => e.DistrictCode == ShippingAddress.District.DistrictCode);
			ShippingAddress.Ward = CurrentWardList[0];
			StateHasChanged();
		}

		protected void HandleSelectedWardChanged(WardDto newValue)
		{
			ShippingAddress.Ward = newValue;
			StateHasChanged();
		}

		protected void HandleHouseNumberChanged(string newValue)
		{
			ShippingAddress.HouseNumber = newValue;
			StateHasChanged();
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
