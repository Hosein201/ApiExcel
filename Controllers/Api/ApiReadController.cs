using ApiExcel.Repository;
using ApiExcel.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace ApiExcel.Controllers.Api
{
    [Route("api/ApiRead")]
    [ApiController]
    public class ApiReadController : ControllerBase
    {
        private readonly IRepositoryRead iRepositoryRead;
        public ApiReadController(IRepositoryRead _iRepositoryRead)
        {
            iRepositoryRead = _iRepositoryRead;
        }
        [HttpGet("Read")]
        public IActionResult Read()
        {
            var data =  iRepositoryRead.Get();
            return Ok(new ApiResult(true, data, "عملیات با موفیت انجام شد"));
        }
    }
}
