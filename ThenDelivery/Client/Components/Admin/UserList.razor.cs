using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Common;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Components.Admin
{
	[Authorize(Roles = Const.Role.AdministrationRole)]
	public class UserListBase : CustomComponentBase<UserListBase>
	{
		#region Inject

		#endregion

		#region Properties
		protected List<UserDto> UserList { get; set; }
		#endregion

		#region Life Cycle
		protected override async Task OnInitializedAsync()
		{
			Logger.LogDebug("Life Cycle - OnInitializedAsync");

			UserList = await HttpClientServer.CustomGetAsync<List<UserDto>>("api/user");
		}
		#endregion

		#region Event Handler
		protected void HandleOnEditUser(UserDto userToEdit)
		{
			if (userToEdit is null)
			{
			}
		}

		protected void HandleOnDeleteUser(UserDto userToDelete)
		{
			if (userToDelete is null)
			{
			}
		}
		#endregion

		#region Methods

		#endregion
	}
}
