using System.Collections.Generic;
using System.Threading.Tasks;
using ThenDelivery.Client.Components;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Shared.Enums;
using ThenDelivery.Shared.Exceptions;

namespace ThenDelivery.Client.Pages
{
	public class MyMerchantsBase : CustomComponentBase<MyMerchantsBase>
	{
		public List<MerchantDto> MyMerchantList { get; set; }
		public CustomResponse ResponseModel { get; set; }

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			MyMerchantList =
				await HttpClientServer.CustomGetAsync<List<MerchantDto>>("api/Merchant/my");
		}

		protected void HandleMoveToOrderPage(int merchantId)
		{
			NavigationManager.NavigateTo($"/order-of-merchant/{merchantId}");
		}

		protected void HandleMoveToEditMenuPage(int merchantId)
		{
			NavigationManager.NavigateTo($"/merchant-edit/{merchantId}");
		}

		protected async Task HandleCloseMerchant(MerchantDto merchant)
		{
			ResponseModel = await HttpClientServer.CustomPutAsync("api/merchant/close", merchant);
			if (ResponseModel.IsSuccess)
			{
				merchant.Status = MerchantStatus.Closed;
			}
			else
			{
				MyMerchantList =
					await HttpClientServer.CustomGetAsync<List<MerchantDto>>("api/Merchant/my");
				await InvokeAsync(StateHasChanged);
			}
			ResponseModel.IsShowPopup = true;
		}

		protected async Task HandleOpenMerchant(MerchantDto merchant)
		{
			ResponseModel = await HttpClientServer.CustomPutAsync("api/merchant/open", merchant);
			if (ResponseModel.IsSuccess)
			{
				merchant.Status = MerchantStatus.Approved;
			}
			else
			{
				MyMerchantList =
					await HttpClientServer.CustomGetAsync<List<MerchantDto>>("api/Merchant/my");
				await InvokeAsync(StateHasChanged);
			}
			ResponseModel.IsShowPopup = true;
		}
	}
}
