namespace ThenDelivery.Server.Application.Common.Interfaces
{
	public interface IImageService
	{
		string SaveImage(string base64String, string path);
	}
}
