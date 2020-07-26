using System;
using System.ComponentModel.DataAnnotations;

namespace ThenDelivery.Shared.Common
{
	public interface IAuditableEntity
	{
		[StringLength(256)]
		public string CreatedBy { get; set; }
		public DateTime Created { get; set; }
		[StringLength(256)]
		public string LastModifiedBy { get; set; }
		public DateTime? LastModified { get; set; }
		public bool IsDeleted { get; set; }
	}

	public class AuditableEntity : IAuditableEntity
	{
		[StringLength(256)]
		public string CreatedBy { get; set; }
		public DateTime Created { get; set; }
		[StringLength(256)]
		public string LastModifiedBy { get; set; }
		public DateTime? LastModified { get; set; }
		public bool IsDeleted { get; set; }
	}
}
