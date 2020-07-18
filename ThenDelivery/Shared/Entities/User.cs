using Microsoft.AspNetCore.Identity;
using System;
using ThenDelivery.Shared.Common;

namespace ThenDelivery.Shared.Entities
{
	public class User : IdentityUser, IAuditableEntity
	{
		public DateTime BirthDate { get; set; }

		public string CreatedBy { get; set; }
		public DateTime Created { get; set; }
		public string LastModifiedBy { get; set; }
		public DateTime? LastModified { get; set; }
		public bool IsDeleted { get; set; }
	}
}
