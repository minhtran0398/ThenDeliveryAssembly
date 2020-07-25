using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
		public MerchantDto MerchantModel { get; set; } = new MerchantDto();
		#endregion

		#region Properties
		public EditContext EditContext { get; set; }
		public List<CityDto> CityList { get; set; }
		public List<DistrictDto> DistrictList { get; set; }
		public List<WardDto> WardList { get; set; }
		#endregion

		#region Life Cycle
		protected override async Task OnInitializedAsync()
		{
			Logger.LogInformation("OnInitializedAsync");

			EditContext = new EditContext(MerchantModel);
			await LoadDataCombobox();
		}
		#endregion

		#region Events
		protected async Task HandleOnSubmit()
		{
			Logger.LogInformation("HandleOnSubmit");
			MerchantModel.Avatar = "";
			MerchantModel.CoverPicture = "";

			var returnedId = await HttpClient.CustomPostAsync($"{BaseUrl}api/merchant", MerchantModel);
			if (returnedId == null)
			{

			}
			else
			{

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
			MerchantModel.OpenTime = newTime;
		}

		protected void HandleCloseTimeChanged(CustomTime newTime)
		{
			MerchantModel.CloseTime = newTime;
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

		protected void HandleHouseNumberChanged(string newValue)
		{
			MerchantModel.HouseNumber = newValue;
		}
		#endregion

		#region Methods
		private async Task LoadDataCombobox()
		{
			CityList = (await HttpClient.CustomGetAsync<CityDto>($"{BaseUrl}api/city")).ToList();
			DistrictList = (await HttpClient.CustomGetAsync<DistrictDto>($"{BaseUrl}api/district")).ToList();
			WardList = (await HttpClient.CustomGetAsync<WardDto>($"{BaseUrl}api/ward")).ToList();
		}
		#endregion
	}
}
