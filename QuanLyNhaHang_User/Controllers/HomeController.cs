using Microsoft.AspNetCore.Mvc;
using QuanLyNhaHang_User.Models;
using QuanLyNhaHang_User.Sevices;
using System.Diagnostics;
using System.Threading.Tasks;

namespace QuanLyNhaHang_User.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApiService _apiService;

        public HomeController(ILogger<HomeController> logger, IApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }
        public IActionResult Service()
        {
            // Check if the user ID cookie exists
            if (HttpContext.Session.GetInt32("userId") != null)
            {
                // If the user ID is already set in the session, redirect to the service page
                return View("Service", HttpContext.Session.GetString("userName"));
            }
            else
            {
                return RedirectToAction("UserLogin", "Home");
            }
        }
        public async Task<IActionResult> UserLoginAsync(int tableId)
        {
            Request.Cookies.TryGetValue("userId", out string? userInfo);
            HttpContext.Session.SetInt32("tableId", tableId);
            // If the cookie is not set, redirect to the login page
            if (string.IsNullOrEmpty(userInfo))
            {
                return View();
            }

            var result = await _apiService.GetUserInformation(userInfo);

            if (result.IsSussess && result.Data != null)
            {
                // Set the user ID in the session & set the user name in the session
                HttpContext.Session.SetInt32("userId", result.Data.KhId);
                HttpContext.Session.SetString("userName", result.Data.TenKhachHang);

                var order = await _apiService.CheckOrderOrInitOrder(result.Data.KhId, tableId);
                if (order.Data != null)
                {
                    HttpContext.Session.SetInt32("orderId", order.Data.DhId);
                }
                return RedirectToAction("Service", "Home");
            }
            else
            {
                return RedirectToAction("UserLogin");
            }
        }
        public async Task<IActionResult> InitCookie(string userName)
        {
            var response = await _apiService.PostUserCreate(userName);

            // Ensure response.Data is not null before accessing KhId
            if (response.Data != null)
            {
                // Set the user ID in the session
                HttpContext.Session.SetInt32("userId", response.Data.KhId);
                HttpContext.Session.SetString("userName", response.Data.TenKhachHang);
                // Set the user ID in a cookie for 7 days
                Response.Cookies.Append("userId", response.Data.KhId.ToString(), new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddDays(7)
                });
                return RedirectToAction("Service", "Home");
            }
            else
            {
                // Handle the case where response.Data is null
                _logger.LogError("Failed to create user. Response data is null.");
                return BadRequest("Failed to create user.");
            }
        }
        public async Task<IActionResult> ProductDetail(int productId)
        {
            var product = await _apiService.GetProductById(productId);
            if (product.Data != null)
            {
                ProductOrderPageViewModel productOrderPageViewModel = new ProductOrderPageViewModel
                {
                    Product = product.Data,
                    OrderInfo = new ProductOrder()
                };
                return View("ProductDetail", productOrderPageViewModel);
            }
            else
            {
                _logger.LogError("Product not found with ID: {ProductId}", productId);
                return NotFound("Product not found.");
            }
        }
        public IActionResult Menu()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Order(ProductOrderPageViewModel productOrderPageViewModel)
        {
          
            var response = await _apiService.AddOrderDetailOnOrder(new RequestOrderDetail
            {
                DhId = HttpContext.Session.GetInt32("orderId") ?? 0,
                ProductId = productOrderPageViewModel.OrderInfo.ProductID,
                SoLuong = productOrderPageViewModel.OrderInfo.SoLuong,
                Ghichu = "Ghi chú: " + productOrderPageViewModel.OrderInfo.GhiChu + " , Trạng thái: " + productOrderPageViewModel.OrderInfo.Conditions
            });
            if (response.IsSussess)
            {
                _logger.LogInformation("Order detail added successfully for Product ID: {ProductId}", productOrderPageViewModel.OrderInfo.ProductID);
                return RedirectToAction("Menu", "Home");
            }
            else
            {
                _logger.LogError("Failed to add order detail for Product ID: {ProductId}. Error: {ErrorMessage}", productOrderPageViewModel.OrderInfo.ProductID, response.Message);
                ModelState.AddModelError("", "Failed to add order detail. Please try again.");
                return BadRequest("Null productId");
            }
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Increase(int quantity , int productId , int orderId)
        {
            var response =await _apiService.OrderDetailMore(HttpContext.Session.GetInt32("userId") ?? 0, HttpContext.Session.GetInt32("orderId") ?? 0, productId, 1);
            if (response.IsSussess && response.Data != null)
            {
                _logger.LogInformation("Increased quantity for Product ID: {ProductId} to {Quantity}", productId, quantity);
                return Json(new { success = true, newQuantity = response.Data.SoLuong});
            }
            else
            {
                _logger.LogError("Failed to increase quantity for Product ID: {ProductId}. Error: {ErrorMessage}", productId, response.Message);
                ModelState.AddModelError("", "Failed to increase quantity. Please try again.");
                return BadRequest("Null productId");
            }
        }
        public async Task<IActionResult> Reduce(int quantity, int productId, int orderId)
        {
            var response = await _apiService.OrderDetailReduce(HttpContext.Session.GetInt32("userId") ?? 0, HttpContext.Session.GetInt32("orderId") ?? 0, productId, 1);
            if (response.IsSussess && response.Data != null)
            {
                _logger.LogInformation("Reduced quantity for Product ID: {ProductId} to {Quantity}", productId, quantity);
                return Json(new { success = true, newQuantity = response.Data.SoLuong });
            }
            else
            {
                _logger.LogError("Failed to reduce quantity for Product ID: {ProductId}. Error: {ErrorMessage}", productId, response.Message);
                ModelState.AddModelError("", "Failed to reduce quantity. Please try again.");
                return BadRequest("Null productId");
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetProductPartial(int productId)
        {
            var isOrderDetailRemove =await _apiService.DeleteOrderDetail(HttpContext.Session.GetInt32("orderId") ?? 0, productId);
            if(isOrderDetailRemove.IsSussess && isOrderDetailRemove.Data == true)
            {
                return PartialView("ButtonAddOrderDetailPartialView", productId);
            }
            return Json(new { success = false, message = "Failed to remove order detail." });
        }
    }
}
