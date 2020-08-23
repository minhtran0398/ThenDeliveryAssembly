namespace ThenDelivery.Shared.Exceptions
{
	public class CustomResponse
	{
		public CustomResponse(int statusCode, string message)
		{
			StatusCode = statusCode;
			Message = message;
			IsShowPopup = false;
		}

		public int StatusCode { get; set; }
		public string Message { get; set; }
      public bool IsShowPopup { get; set; }
      public bool IsSuccess
		{
			get
			{
				if (StatusCode >= 200 && StatusCode < 300)
				{
					return true;
				}
				return false;
			}
		}
	}
}