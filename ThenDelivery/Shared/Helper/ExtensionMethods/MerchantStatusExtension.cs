using ThenDelivery.Shared.Enums;

namespace ThenDelivery.Shared.Helper.ExtensionMethods
{
   public static class MerchantStatusExtension
   {
      public static string GetStringValue(this MerchantStatus status)
      {
         return status switch
         {
            MerchantStatus.NotApproved => "Đang xác thực",
            MerchantStatus.Approved => "Đang hoạt động",
            MerchantStatus.Closed => "Đã đóng cửa",
            _ => string.Empty
         };
      }

      public static string GetCssClass(this MerchantStatus status)
      {
         return status switch
         {
            MerchantStatus.NotApproved => "badge badge-pill badge-primary",
            MerchantStatus.Approved => "badge badge-pill badge-success",
            MerchantStatus.Closed => "badge badge-pill badge-danger",
            _ => string.Empty,
         };
      }
   }
}
