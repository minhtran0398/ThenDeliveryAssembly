using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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

			UserList = (await HttpClient.CustomGetAsync<UserDto>($"{BaseUrl}api/user")).ToList();
		}
		#endregion

		#region Event Handler
		protected void HandleOnEditUser(UserDto userToEdit)
		{

		}

		protected void HandleOnDeleteUser(UserDto userToDelete)
		{

		}
		#endregion

		#region Methods
		
		#endregion
	}
}
