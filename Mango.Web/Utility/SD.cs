namespace Mango.Web.Utility
{
    public class Sd
    {
        public static string CouponApiBase { get; set; } = "api/coupon";

        public enum ApiType
        {
            Get,
            Post,
            Put,
            Patch,
            Delete
        }
    }
}
