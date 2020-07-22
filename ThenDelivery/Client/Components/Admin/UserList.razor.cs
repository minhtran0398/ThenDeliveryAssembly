using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
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
		#region Inject
		[Inject] public ILogger<UserListBase> Logger { get; set; }
		[Inject] public HttpClient HttpClient { get; set; }
		[Inject] public NavigationManager NavigationManager { get; set; }
		#endregion

		#region Properties
		public string BaseUrl { get; set; }
		protected List<UserDto> UserList { get; set; }
		#endregion

		#region Life Cycle
		protected override async Task OnInitializedAsync()
		{
			Logger.LogDebug("Life Cycle - OnInitializedAsync");
			SetupProperties();

			HttpResponseMessage response = await HttpClient.GetAsync($"{BaseUrl}api/user");
			if(response.IsSuccessStatusCode)
			{
				UserList = await response.Content.ReadFromJsonAsync<List<UserDto>>();
			}
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
		private void SetupProperties()
		{
			BaseUrl = NavigationManager.BaseUri;
			Logger.LogDebug(BaseUrl);
		}
		#endregion
	}
}
