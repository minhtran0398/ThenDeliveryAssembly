using Microsoft.AspNetCore.Components;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Components.Admin
{
	public class UserLineBase : ComponentBase
	{
		#region Parameters
		[Parameter] public UserDto UserItem { get; set; }
		[Parameter] public EventCallback<UserDto> OnEdit { get; set; }
		[Parameter] public EventCallback<UserDto> OnDelete { get; set; }
		#endregion

		#region LifeCycle

		#endregion

		#region Event Handler
		protected void HandleOnEdit() => OnEdit.InvokeAsync(UserItem);
		protected void HandleOnDelete() => OnDelete.InvokeAsync(UserItem);
		#endregion
	}
}
