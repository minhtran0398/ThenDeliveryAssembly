using System;

namespace ThenDelivery.Shared.Dtos
{
	public class UserDto
	{
		public string UserName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public bool IsEmailConfirmed { get; set; }
		public bool IsPhoneNumberConfirmed { get; set; }
		public DateTime BirthDate { get; set; }
	}
}
