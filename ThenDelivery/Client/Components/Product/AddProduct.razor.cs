using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Components.Product
{
	public class AddProductBase : CustomComponentBase<AddProductBase>
	{
		#region Inject

		#endregion

		#region Parameters
		#endregion

		#region Properties
		public List<ProductDto> ProductList { get; set; }
		public bool IsShowPopupAddProduct { get; set; }
		public bool IsSave { get; set; }
		#endregion

		#region Variables

		#endregion

		#region Life Cycle
		protected override void OnInitialized()
		{
			// do not remove this line
			base.OnInitialized();

			ProductList = new List<ProductDto>();
		}
		#endregion

		#region Events
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
