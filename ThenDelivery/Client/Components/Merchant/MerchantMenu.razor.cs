using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Shared.Helper;

namespace ThenDelivery.Client.Components.Merchant
{
	public class MerchantMenuBase : CustomComponentBase<MerchantMenuBase>
	{
		#region Inject

		#endregion

		#region Parameters
		[Parameter] public MerchantDto MerchantModel { get; set; }
		#endregion

		#region Properties
		public ObservableCollection<StoreMenuDto> MenuList { get; set; }
		protected bool IsEnableAddButton { get; set; }
		protected bool IsEnableSaveButton { get; set; }
		protected string NewMenuItemName { get; set; }
		#endregion

		#region Life Cycle
		protected override void OnInitialized()
		{
			// do not remove this line
			base.OnInitialized();
			MenuList = new ObservableCollection<StoreMenuDto>();

			MenuList.CollectionChanged += HandleMenuListCollectionChanged;
		}
		#endregion

		#region Events
		private void HandleMenuListCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if(e.Action == NotifyCollectionChangedAction.Add)
			{
				IsEnableSaveButton = true;
			}
			else if(e.Action == NotifyCollectionChangedAction.Remove)
			{
				IsEnableSaveButton = false;
			}
		}

		protected void HandleAddMenuItem(MouseEventArgs mouseArgs)
		{
			AddMenuItem();
		}

		/// <summary>
		/// Todo: Current doesn't work
		/// </summary>
		/// <param name="keyboardArgs"></param>
		protected void HandleOnPressMenuItem(KeyboardEventArgs keyboardArgs)
		{
			if(keyboardArgs.Key == "Enter")
			{
				AddMenuItem();
			}
			else
			{
				IsEnableAddButton = true;
			}
		}
		#endregion

		#region Methods
		private void AddMenuItem()
		{
			if (String.IsNullOrWhiteSpace(NewMenuItemName) == false)
			{
				MenuList.Add(new StoreMenuDto()
				{
					Name = NewMenuItemName
				});
				NewMenuItemName = String.Empty;
				StateHasChanged();
			}
		}
		#endregion
	}
}
