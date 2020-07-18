using System;

namespace ThenDelivery.Shared.Common
{
	public interface IAuditableEntity
	{
		public string CreatedBy { get; set; }
		public DateTime Created { get; set; }
		public string LastModifiedBy { get; set; }
		public DateTime? LastModified { get; set; }
		public bool IsDeleted { get; set; }
	}

	public class AuditableEntity : IAuditableEntity
	{
		public string CreatedBy { get; set; }
		public DateTime Created { get; set; }
		public string LastModifiedBy { get; set; }
		public DateTime? LastModified { get; set; }
		public bool IsDeleted { get; set; }
	}
}
