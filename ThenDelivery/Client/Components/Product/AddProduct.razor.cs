using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Components.Product
{
	public class AddProductBase : CustomComponentBase<AddProductBase>
	{
		#region Inject

		#endregion

		#region Parameters
		[Parameter] public EventCallback<bool> OnChangeTab { get; set; }
		#endregion

		#region Properties
		public List<MerchantMenuDto> MenuList { get; set; }
		public ObservableCollection<ProductDto> ProductList { get; set; }
		public bool IsShowPopupAddProduct { get; set; }
		public bool IsEnableSaveButton { get; set; }
		#endregion

		#region Variables

		#endregion

		#region Life Cycle
		protected override void OnInitialized()
		{
			// do not remove this line
			base.OnInitialized();

			ProductList = new ObservableCollection<ProductDto>();
			ProductList.CollectionChanged += HandleProductListChanged;
		}

		protected override async Task OnInitializedAsync()
		{
			MenuList = (await HttpClient
				.CustomGetAsync<MerchantMenuDto>($"{BaseUrl}api/merchantmenu?merchantId={3}")).ToList();
		}
		#endregion

		#region Events
		private void HandleProductListChanged(object sender, NotifyCollectionChangedEventArgs e)
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

		protected void HandleShowProductForm()
		{
			IsShowPopupAddProduct = true;
		}

		protected void HandleAddProduct(ProductDto product)
		{
			AddProduct(product);
			IsShowPopupAddProduct = false;
		}

		protected void HandleCancelAddProduct(bool isShowForm)
		{
			IsShowPopupAddProduct = isShowForm;
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
		private void AddProduct(ProductDto product)
		{
			if(product != null)
			{
				ProductList.Add(product);
				StateHasChanged();
			}
		}
		#endregion
	}
}
