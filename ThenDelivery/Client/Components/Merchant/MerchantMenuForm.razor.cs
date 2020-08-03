using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Client.Components.Enums;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Components.Merchant
{
	public class MerchantMenuFormBase : CustomComponentBase<MerchantMenuFormBase>
	{
		#region Inject

		#endregion

		#region Parameters
		[Parameter] public int TargetMerchantId { get; set; }
		[Parameter] public EventCallback<PageAction> OnChangeTab { get; set; }
		#endregion

		#region Properties
		public ObservableCollection<MenuItemDto> MenuList { get; set; }
		protected bool IsEnableAddButton { get; set; }
		protected bool IsEnableSaveButton { get; set; }
		protected string NewMenuItemName { get; set; }
		#endregion

		#region Variables
		private int _merchantMenuIdToEdit = -1;
		#endregion

		#region Life Cycle
		protected override void OnInitialized()
		{
			// do not remove this line
			base.OnInitialized();
			MenuList = new ObservableCollection<MenuItemDto>();
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
		/// <param name="merchantMenuId"></param>
		protected void HandleRemoveMenuItem(int merchantMenuId)
		{
			MenuList.RemoveFirst(s => s.Id == merchantMenuId);
		}

		/// <summary>
		/// <para>Use merchantId to save id of the row, this merchantId is not for save</para>
		/// <para>it will be updated in CQRS command</para>
		/// </summary>
		/// <param name="merchantMenuId"></param>
		protected void HandleEditMenuItem(int merchantMenuId)
		{
			NewMenuItemName = MenuList.Single(s => s.Id == merchantMenuId).Name;
			_merchantMenuIdToEdit = merchantMenuId;
		}

		protected async Task HandleTurnBack()
		{
			await OnChangeTab.InvokeAsync(PageAction.Previous);
		}

		/// <summary>
		/// Call api save data here
		/// </summary>
		protected async Task HandleSaveAndContinue()
		{
			await HttpClient.CustomPostAsync($"{BaseUrl}api/merchantmenu", MenuList.AsEnumerable());
			await OnChangeTab.InvokeAsync(PageAction.Next);
		}
		#endregion

		#region Methods
		private void AddMenuItem()
		{
			if (String.IsNullOrWhiteSpace(NewMenuItemName) == false)
			{
				// mode add
				if (_merchantMenuIdToEdit == -1)
				{
					int merchantIdToAdd = 1;
					if (MenuList.Count > 0)
					{
						merchantIdToAdd = MenuList.Max(e => e.Id) + 1;
					}
					MenuList.Add(new MenuItemDto()
					{
						Id = merchantIdToAdd,
						Name = NewMenuItemName,
						MerchantId = TargetMerchantId
					});
				}
				// mode edit
				else
				{
					MenuList.Single(e => e.Id == _merchantMenuIdToEdit).Name = NewMenuItemName;
					_merchantMenuIdToEdit = -1;
				}
				NewMenuItemName = String.Empty;
				IsEnableAddButton = false;
				StateHasChanged();
			}
		}
		#endregion
	}
}
