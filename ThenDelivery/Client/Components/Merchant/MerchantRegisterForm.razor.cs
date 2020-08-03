using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Shared.Helper;

namespace ThenDelivery.Client.Components.Merchant
{
	public class MerchantRegisterFormBase : CustomComponentBase<MerchantRegisterFormBase>
	{
		#region Inject

		#endregion

		#region Parameters
		[Parameter] public MerchantDto MerchantModel { get; set; }
		[Parameter] public EventCallback<int> OnSubmitMerchant { get; set; }
		#endregion

		#region Properties
		protected EditContext EditContext { get; set; }
		#endregion

		#region Life Cycle
		protected override void OnInitialized()
		{
			EditContext = new EditContext(MerchantModel);
		}
		#endregion

		#region Events
		protected async Task HandleOnSubmit()
		{
			var returnedId = await HttpClientServer.CustomPostAsync($"{BaseUrl}api/merchant", MerchantModel);
			if (returnedId == null)
			{
				await OnSubmitMerchant.InvokeAsync(-1);
			}
			else
			{
				await OnSubmitMerchant.InvokeAsync(int.Parse(returnedId.ToString()));
			}
		}

		protected void HandleMerchantNameChanged(string newValue)
		{
			MerchantModel.Name = newValue;
		}

		protected void HandleTaxCodeChanged(string newValue)
		{
			MerchantModel.TaxCode = newValue;
		}

		protected void HandlePhoneNumberChanged(string newValue)
		{
			MerchantModel.PhoneNumber = newValue;
		}

		protected void HandleSearchKeyChanged(string newValue)
		{
			MerchantModel.SearchKey = newValue;
		}

		protected void HandleDescriptionChanged(string newValue)
		{
			MerchantModel.Description = newValue;
		}

		protected void HandleOpenTimeChanged(CustomTime newTime)
		{
			MerchantModel.OpenTime.Hour = newTime.Hour;
			MerchantModel.OpenTime.Minute = newTime.Minute;
		}

		protected void HandleCloseTimeChanged(CustomTime newTime)
		{
			MerchantModel.CloseTime.Hour = newTime.Hour;
			MerchantModel.CloseTime.Minute = newTime.Minute;
		}

		protected void HandleSelectedCityChanged(CityDto newValue)
		{
			MerchantModel.City = newValue;
		}

		protected void HandleSelectedDistrictChanged(DistrictDto newValue)
		{
			MerchantModel.District = newValue;
		}

		protected void HandleSelectedWardChanged(WardDto newValue)
		{
			MerchantModel.Ward = newValue;
		}

		protected void HandleSelectedMerchantTypeChanged(IEnumerable<MerTypeDto> newValue)
		{
			MerchantModel.MerchantTypeList = newValue.ToList();
		}

		protected void HandleSelectedFeaturedDishCategoryChanged(IEnumerable<FeaturedDishDto> newValue)
		{
			MerchantModel.FeaturedDishCategoryList = newValue.ToList();
		}

		protected void HandleHouseNumberChanged(string newValue)
		{
			MerchantModel.HouseNumber = newValue;
		}

		protected void HandleAvatarChanged(string newValue)
		{
			MerchantModel.Avatar = newValue;
		}

		protected void HandleCoverPictureChanged(string newValue)
		{
			MerchantModel.CoverPicture = newValue;
		}

		#endregion

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
