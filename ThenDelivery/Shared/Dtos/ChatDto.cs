using System;
using System.Collections.Generic;

namespace ThenDelivery.Shared.Dtos
{
	public class ChatDto
	{
		public int Id { get; set; }
		public UserDto Client1 { get; set; }
		public UserDto Client2 { get; set; }
		public DateTime CreateAt { get; set; }

		public List<ChatMessageDto> MessagesList { get; set; }
	}
}
