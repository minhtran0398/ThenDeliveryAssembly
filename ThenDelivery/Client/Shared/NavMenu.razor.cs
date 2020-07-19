using Microsoft.AspNetCore.Components;

namespace ThenDelivery.Client.Shared
{
	public class NavMenuBase : ComponentBase
	{
		protected bool collapseNavMenu = true;

		protected string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

		protected void ToggleNavMenu()
		{
			collapseNavMenu = !collapseNavMenu;
		}
	}
}
