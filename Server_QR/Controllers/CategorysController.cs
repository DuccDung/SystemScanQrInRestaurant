using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server_QR.Services;

namespace Server_QR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorysController : ControllerBase
    {
        private readonly IApiService _apiService;
        public CategorysController(IApiService apiService)
        {
            _apiService = apiService;
        }
        [Route("GetAllCategory")]
        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var result = await _apiService.GetAllCategory();
            if (result.IsSussess && result.DataList != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound("No member categories found.");
            }
        }
    } 
}
