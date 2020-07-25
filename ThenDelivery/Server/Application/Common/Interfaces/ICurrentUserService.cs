namespace ThenDelivery.Server.Application.Common.Interfaces
{
	public interface ICurrentUserService
	{
		string UserName { get; }
		string GetLoggedInUserId();
	}
}
