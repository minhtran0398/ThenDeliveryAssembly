using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;

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
		protected void HandleOnSubmit()
		{
			Logger.LogInformation("HandleOnSubmit");
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
