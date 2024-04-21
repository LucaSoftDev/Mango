using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
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
                var ret = db.Coupons.FirstOrDefault(u => u.CouponCode.ToLower() == code.ToLower());
                _response.Result = mapper.Map<CouponDto>(ret);
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
