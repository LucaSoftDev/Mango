using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class CouponController(ICouponService couponService) : Controller
    {
        private readonly ICouponService _couponService = couponService;

        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto>? list = [];
            ResponseDto? response = await _couponService.GetAllCouponsAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto couponDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _couponService.CreateCouponAsync(couponDto);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }
            }
            return View();
        }

        public async Task<IActionResult> CouponDelete(int id)
        {
            ResponseDto? response = await _couponService.GetCouponByIdAsync(id);

            if (response != null && response.IsSuccess)
            {
                CouponDto? couponDto = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
                return View(couponDto);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDto couponDto)
        {
            ResponseDto? response = await _couponService.DeleteCouponAsync(couponDto.Id);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(CouponIndex));
            }
            return View(couponDto);
        }
    }
}
