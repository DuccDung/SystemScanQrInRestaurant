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
        [HttpGet]
        [Route("CheckOrderDetailExist")]
        public async Task<IActionResult> CheckOrderDetailExist(int userId, int orderId, int tableId, int productId)
        {
            return Ok(await _apiService.CheckOrderDetailExist(userId, orderId, tableId, productId));
        }

        [HttpGet]
        [Route("CheckOrderExistOrInitOrder")]
        public async Task<IActionResult> CheckOrderExistOrInitOrder(int userId, int tableId)
        {
            return Ok(await _apiService.CheckOrInitOrder(userId, tableId));
        }

        [HttpGet]
        [Route("OrderDetailMore")]
        public async Task<IActionResult> OrderDetailMore(int userId, int orderId, int productId, int quantiy)
        {
            var response = await _apiService.OrderDetailMore(userId, orderId, productId, quantiy);
            if (response.IsSussess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
        [HttpGet]
        [Route("OrderDetailReduce")]
        public async Task<IActionResult> OrderDetailReduce(int userId, int orderId, int productId, int quantiy)
        {
            var response = await _apiService.OrderDetailReduce(userId, orderId, productId, quantiy);
            if (response.IsSussess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteOrderDetail")]
        public async Task<IActionResult> DeleteOrderDetail(int orderId, int productId)
        {
            var response = await _apiService.DeleteOrderDetail(orderId, productId);
            if (response.IsSussess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
        [HttpGet]
        [Route("CountOrderDetailInOrder")]
        public async Task<IActionResult> CountOrderDetailInOrder(int orderId)
        {
            var response = await _apiService.CountOrderDetailInOrder(orderId);
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
