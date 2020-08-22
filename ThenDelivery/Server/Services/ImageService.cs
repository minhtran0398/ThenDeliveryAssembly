using System;
using System.IO;
using ThenDelivery.Server.Application.Common.Interfaces;

namespace ThenDelivery.Server.Services
{
	public class ImageService : IImageService
	{
		public string SaveImage(string base64String, string path)
		{
			string fileName = @$"wwwroot\Images\{path}\{Guid.NewGuid()}.jpg";
			try
			{
				var byteArr = Convert.FromBase64String(base64String);
				using MemoryStream ms = new MemoryStream(byteArr);
				using FileStream file = new FileStream(fileName, FileMode.Create, FileAccess.Write);
				ms.CopyTo(file);
			}
			catch (Exception)
			{
				throw;
			}
			return fileName.Substring(8);
		}
	}
}
