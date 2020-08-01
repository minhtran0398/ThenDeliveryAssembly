using BlazorInputFile;
using Microsoft.AspNetCore.Components;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ThenDelivery.Client.Components.Commons
{
	public class CustomImageEditBase : ComponentBase
	{
		[Parameter] public short Width { get; set; } = 300;
		[Parameter] public string Base64String { get; set; }
		[Parameter] public EventCallback<string> OnChangeImage { get; set; }

		protected string imageData;

		protected async Task HandleFileSelected(IFileListEntry[] files)
		{
			var file = files[0];
			using MemoryStream ms = await file.ReadAllAsync();
			Base64String = Convert.ToBase64String(ms.ToArray());
			imageData = String.Format("data:image/jpg;base64,{0}", Base64String);
			await OnChangeImage.InvokeAsync(imageData);
			await InvokeAsync(StateHasChanged);
		}
	}
}
