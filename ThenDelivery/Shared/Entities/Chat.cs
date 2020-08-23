using System;

namespace ThenDelivery.Shared.Entities
{
	public class Chat
	{
		public int Id { get; set; }
		public string Client1Id { get; set; }
		public string Client2Id { get; set; }
		public DateTime CreateAt { get; set; }
	}
}
