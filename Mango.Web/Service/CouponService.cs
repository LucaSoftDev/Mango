using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
    public class CouponService(IBaseService baseService) : ICouponService
    {
        public async Task<ResponseDto?> GetCouponAsync(string couponCode)
        {
            return await baseService.SendAsync(new RequestDto
            {
                ApiType = Sd.ApiType.Get,
                Url = Sd.CouponApiBase + $"/api/coupon/GetByCode/{couponCode}"
            });
        }

        public async Task<ResponseDto?> GetAllCouponsAsync()
        {
            return await baseService.SendAsync(new RequestDto
            {
                ApiType = Sd.ApiType.Get,
                Url = Sd.CouponApiBase + "/api/coupon"
            }); 
        }

        public async Task<ResponseDto?> GetCouponByIdAsync(int id)
        {
            return await baseService.SendAsync(new RequestDto
            {
                ApiType = Sd.ApiType.Get,
                Url = Sd.CouponApiBase + $"/api/coupon/{id}"
            });
        }

        public async Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto)
        {
            return await baseService.SendAsync(new RequestDto
            {
                ApiType = Sd.ApiType.Post,
                Data = couponDto,
                Url = Sd.CouponApiBase + $"/api/coupon"
            });
        }

        public async Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto)
        {
            return await baseService.SendAsync(new RequestDto
            {
                ApiType = Sd.ApiType.Put,
                Data = couponDto,
                Url = Sd.CouponApiBase + $"/api/coupon"
            });
        }

        public async Task<ResponseDto?> DeleteCouponAsync(int id)
        {
            return await baseService.SendAsync(new RequestDto
            {
                ApiType = Sd.ApiType.Delete,
                Url = Sd.CouponApiBase + $"/api/coupon/{id}"
            });
        }
    }
}
