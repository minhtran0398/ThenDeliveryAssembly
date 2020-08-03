using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Threading.Tasks;

namespace ThenDelivery.Client.Shared
{
	public class LoginDisplayBase : ComponentBase
	{
		[Inject] private NavigationManager Navigation { get; set; }
		[Inject] private SignOutSessionStateManager SignOutManager { get; set; }

		protected async Task BeginSignOut(MouseEventArgs _)
		{
			await SignOutManager.SetSignOutState();
			Navigation.NavigateTo("authentication/logout");
		}
	}
}
