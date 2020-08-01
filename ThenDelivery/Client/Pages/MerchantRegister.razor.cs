using ThenDelivery.Client.Components;
using ThenDelivery.Client.Components.Enums;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Pages
{
	public class MerchantRegisterBase : CustomComponentBase<MerchantRegisterBase>
	{
		public MerchantDto MerchantModel { get; set; }
		public int MerchantModelId { get; set; }
		public int ActiveTabIndex { get; set; }

		protected override void OnInitialized()
		{
			ActiveTabIndex = 0;
			MerchantModel = new MerchantDto();
		}

		protected void HandleAfterSubmitMerchant(int merchantId)
		{
			if (merchantId > 0)
			{
				ActiveTabIndex += 1;
				MerchantModelId = merchantId;
			}
			else
			{

			}
		}

		/// <summary>
		/// <para> state == true => next tab </para>
		/// <para> state == false => previous tab </para>
		/// </summary>
		/// <param name="moveState"></param>
		protected void HandleOnMenuChangeTab(PageAction moveState)
		{
			if (moveState == PageAction.Next)
			{
				ActiveTabIndex += 1;
			}
			else
			{
				ActiveTabIndex -= 1;
			}
		}

		/// <summary>
		/// <para> state == true => next tab </para>
		/// <para> state == false => previous tab </para>
		/// </summary>
		/// <param name="moveState"></param>
		protected void HandleOnProductChangeTab(PageAction moveState)
		{
			// done
			if (moveState == PageAction.Next)
			{
				NavigationManager.NavigateTo("/");
			}
			else
			{
				ActiveTabIndex -= 1;
			}
		}
	}
}
