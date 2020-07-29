using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Components.Merchant
{
	public class MerchantMenuBase : CustomComponentBase<MerchantMenuBase>
	{
		#region Inject

		#endregion

		#region Parameters
		[Parameter] public int TargetMerchantId { get; set; }
		[Parameter] public EventCallback<bool> OnChangeTab { get; set; }
		#endregion

		#region Properties
		public ObservableCollection<MerchantMenuDto> MenuList { get; set; }
		protected bool IsEnableAddButton { get; set; }
		protected bool IsEnableSaveButton { get; set; }
		protected string NewMenuItemName { get; set; }
		#endregion

		#region Variables
		private int _merchantIdToEdit = -1;
		#endregion

		#region Life Cycle
		protected override void OnInitialized()
		{
			// do not remove this line
			base.OnInitialized();
			MenuList = new ObservableCollection<MerchantMenuDto>();
			MenuList.CollectionChanged += HandleMenuListChanged;
		}
		#endregion

		#region Events
		private void HandleMenuListChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Add)
			{
				IsEnableSaveButton = true;
			}
			else if (e.Action == NotifyCollectionChangedAction.Remove)
			{
				if (MenuList.Count == 0)
					IsEnableSaveButton = false;
			}
		}

		protected void HandleBtnAddMenuItem()
		{
			AddMenuItem();
		}

		/// <summary>
		/// Use DxTextBox mode OnInput to bind-value every time you press a char
		/// </summary>
		/// <param name="keyboardArgs"></param>
		protected void HandleOnKeyupMenuItem(KeyboardEventArgs keyboardArgs)
		{
			Logger.LogInformation("HandleOnPressMenuItem");
			if (keyboardArgs.Key == "Enter")
			{
				AddMenuItem();
			}
			else
			{
				if (String.IsNullOrWhiteSpace(NewMenuItemName))
					IsEnableAddButton = false;
				else
					IsEnableAddButton = true;
			}
		}

		/// <summary>
		/// <para>Use merchantId to save id of the row, this merchantId is not for save</para>
		/// <para>it will be updated in CQRS command</para>
		/// </summary>
		/// <param name="merchantId"></param>
		protected void HandleRemoveMenuItem(int merchantId)
		{
			MenuList.RemoveFirst(s => s.MerchantId == merchantId);
		}

		/// <summary>
		/// <para>Use merchantId to save id of the row, this merchantId is not for save</para>
		/// <para>it will be updated in CQRS command</para>
		/// </summary>
		/// <param name="merchantId"></param>
		protected void HandleEditMenuItem(int merchantId)
		{
			NewMenuItemName = MenuList.Single(s => s.MerchantId == merchantId).Name;
			_merchantIdToEdit = merchantId;
		}

		protected async Task HandleTurnBack()
		{
			await OnChangeTab.InvokeAsync(false);
		}

		/// <summary>
		/// Call api save data here
		/// </summary>
		protected async Task HandleSaveAndContinue()
		{
			await OnChangeTab.InvokeAsync(true);
		}
		#endregion

		#region Methods
		private void AddMenuItem()
		{
			if (String.IsNullOrWhiteSpace(NewMenuItemName) == false)
			{
				// mode add
				if (_merchantIdToEdit == -1)
				{
					int merchantIdToAdd = 1;
					if (MenuList.Count > 0)
					{
						merchantIdToAdd = MenuList.Max(e => e.MerchantId) + 1;
					}
					MenuList.Add(new MerchantMenuDto()
					{
						MerchantId = merchantIdToAdd,
						Name = NewMenuItemName
					});
				}
				// mode edit
				else
				{
					MenuList.Single(e => e.MerchantId == _merchantIdToEdit).Name = NewMenuItemName;
					_merchantIdToEdit = -1;
				}
				NewMenuItemName = String.Empty;
				IsEnableAddButton = false;
				StateHasChanged();
			}
		}
		#endregion
	}
}
