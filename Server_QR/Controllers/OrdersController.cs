using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server_QR.Models;
using Server_QR.Services;

namespace Server_QR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IApiService _apiService;
        public OrdersController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        [Route("InitializeOrder")]
        public async Task<IActionResult> InitializeOrder(int userId, int tableId)
        {
            await _apiService.InitializeOrder(userId, tableId);
            var response = await _apiService.InitializeOrder(userId, tableId);
            if (response.IsSussess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
        [HttpPost]
        [Route("AddOrderDetailInOrder")]
        public async Task<IActionResult> AddOrderDetailInOrder(RequestOrderDetail requestOrder)
        {
            var response = await _apiService.AddOrderDetailInOrder(requestOrder);
            if (response.IsSussess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

    }
}
