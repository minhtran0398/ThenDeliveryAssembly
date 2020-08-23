using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Client.Components;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Components
{
	public class PopupChatBase : CustomComponentBase<PopupChatBase>
	{
		[Parameter] public UserDto Merchant { get; set; }
		public ChatDto Chat { get; set; }
	}
}