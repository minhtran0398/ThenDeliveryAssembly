using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ThenDelivery.Shared.Common;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Components.Admin
{
	[Authorize(Roles = Const.Role.AdministrationRole)]
	public class UserListBase : ComponentBase
	{
		[Inject] public ILogger<UserListBase> Logger { get; set; }
		[Inject] public HttpClient HttpClient { get; set; }
		protected List<UserDto> UserList { get; set; }

		protected override async Task OnInitializedAsync()
		{
			Logger.LogInformation("Life Cycle - OnInitializedAsync");

			HttpResponseMessage response = await HttpClient.GetAsync("https://localhost:44331/api/user");
			if(response.IsSuccessStatusCode)
			{
				UserList = await response.Content.ReadFromJsonAsync<List<UserDto>>();
			}
		}

		protected void HandleOnEditUser(UserDto userToEdit)
		{

		}

		protected void HandleOnDeleteUser(UserDto userToDelete)
		{

		}
	}
}
