using System;

namespace ThenDelivery.Shared.Entities
{
	public class ChatMessage
	{
		public int ChatId { get; set; }
		public string SendClientId { get; set; }
		public string Text { get; set; }
		public DateTime SendTime { get; set; }
	}
}
