using System.Collections.Generic;

namespace ThenDelivery.Shared.Dtos
{
	public class FeaturedDishDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string DisplayText
		{
			get
			{
				return Name;
			}
		}
	}

	public class FeaturedDishComparerId : IEqualityComparer<FeaturedDishDto>
	{
		public bool Equals(FeaturedDishDto x, FeaturedDishDto y)
		{
			return x.Id == y.Id;
		}

		public int GetHashCode(FeaturedDishDto obj)
		{
			//Check whether the object is null
			if (obj is null) return 0;

			return obj.Id.GetHashCode();
		}
	}
}
