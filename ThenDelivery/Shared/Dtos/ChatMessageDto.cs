using System;

namespace ThenDelivery.Shared.Dtos
{
	public class ChatMessageDto
	{
		public int ChatId { get; set; }
		public UserDto SendClient { get; set; }
		public string Text { get; set; }
		public DateTime SendTime { get; set; }
	}
}
