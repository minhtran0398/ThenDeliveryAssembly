using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Common;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Shared.Enums;
using ThenDelivery.Shared.Exceptions;

namespace ThenDelivery.Client.Components.Admin
{
	[Authorize(Roles = Const.Role.AdministrationRole)]
	public class MerchantListBase : CustomComponentBase<MerchantListBase>
	{
		#region Inject

		#endregion

		#region Properties
		protected List<MerchantDto> MerchantList { get; set; }
		public MerchantDto SelectedMerchant { get; set; }
		public CustomResponse ResponseModel { get; set; }
		public bool IsShowPopupConfirm { get; set; }
		public bool IsShowPopupDeactive { get; set; }
		#endregion

		#region Life Cycle
		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			MerchantList = await HttpClientServer.CustomGetAsync<List<MerchantDto>>("api/merchant/all");
		}
		#endregion

		#region Event Handler

		protected void HandleApproveMerchant(MerchantDto merchant)
		{
			SelectedMerchant = merchant;
			IsShowPopupConfirm = true;
		}

		protected void HandleDeactiveMerchant(MerchantDto merchant)
		{
			SelectedMerchant = merchant;
			IsShowPopupDeactive = true;
		}

		protected async Task HandleConfirmApprove()
		{
			IsShowPopupConfirm = false;
			await InvokeAsync(StateHasChanged);
			ResponseModel = await HttpClientServer.CustomPutAsync($"api/merchant/approve", SelectedMerchant);
			if (ResponseModel.IsSuccess)
			{
				SelectedMerchant.Status = MerchantStatus.Approved;
			}
			ResponseModel.IsShowPopup = true;
			await InvokeAsync(StateHasChanged);
		}

		protected async Task HandleConfirmDeactive()
		{
			IsShowPopupDeactive = false;
			await InvokeAsync(StateHasChanged);
			ResponseModel = await HttpClientServer.CustomPutAsync($"api/merchant/deactive", SelectedMerchant);
			if (ResponseModel.IsSuccess)
			{
				SelectedMerchant.Status = MerchantStatus.AdminClosed;
			}
			ResponseModel.IsShowPopup = true;
			await InvokeAsync(StateHasChanged);
		}

		protected void HandleCancelApprove()
		{
			IsShowPopupConfirm = false;
			IsShowPopupDeactive = false;
		}
		#endregion

		#region Methods

		#endregion
	}
}
