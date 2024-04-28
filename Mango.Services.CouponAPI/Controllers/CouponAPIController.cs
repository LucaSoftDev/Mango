using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponApiController(AppDbContext db, IMapper mapper) : ControllerBase
    {
        private readonly ResponseDto _response = new();

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                _response.Result = mapper.Map<IEnumerable<CouponDto>>(db.Coupons.ToList());
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }
            return _response;
        }

        [HttpGet("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                var ret = db.Coupons.First(u => u.Id == id);
                _response.Result = mapper.Map<CouponDto>(ret);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }
            return _response;
        }

        [HttpGet("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                var ret = db.Coupons.First(u => string.Equals(u.CouponCode, code));
                _response.Result = mapper.Map<CouponDto>(ret);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }
            return _response;
        }

        [HttpPost]
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                var coupon = mapper.Map<Coupon>(couponDto);
                db.Coupons.Add(coupon);
                db.SaveChanges();
                _response.Result = mapper.Map<CouponDto>(coupon);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }

            return _response;
        }

        [HttpPut]
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                var coupon = mapper.Map<Coupon>(couponDto);
                db.Coupons.Update(coupon);
                db.SaveChanges();
                _response.Result = mapper.Map<CouponDto>(coupon);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }

            return _response;
        }

        [HttpDelete("{id:int}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                var coupon = db.Coupons.First(u => u.Id == id);
                db.Coupons.Remove(coupon);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }

            return _response;
        }
    }
}
