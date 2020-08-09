using BlazorInputFile;
using Microsoft.AspNetCore.Components;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ThenDelivery.Client.Components.Commons
{
	public class CustomImageEditBase : ComponentBase
	{
		[Parameter] public ushort Width { get; set; } = 300;
		[Parameter] public ushort Height { get; set; } = 300;
		[Parameter] public string Base64String { get; set; }
		[Parameter] public EventCallback<string> OnChangeImage { get; set; }

		protected string imageData;

		protected async Task HandleFileSelected(IFileListEntry[] files)
		{
			var rawFile = files.FirstOrDefault();
			if (rawFile != null)
			{
				// Load as an image file in memory
				var format = "image/jpeg";
				var imageFile = await rawFile.ToImageFileAsync(format, 640, 480);
				var ms = new MemoryStream();
				await imageFile.Data.CopyToAsync(ms);

				// Make a data URL so we can display it
				Base64String = Convert.ToBase64String(ms.ToArray());
				imageData = $"data:{format};base64,{Base64String}";
				await OnChangeImage.InvokeAsync(Base64String);
				await InvokeAsync(StateHasChanged);
			}
		}
	}
}
