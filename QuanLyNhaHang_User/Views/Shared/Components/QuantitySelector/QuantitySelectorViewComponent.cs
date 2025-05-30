using Microsoft.AspNetCore.Mvc;
using QuanLyNhaHang_User.Models;
using QuanLyNhaHang_User.Sevices;

namespace QuanLyNhaHang_User.Views.Shared.Components.QuantitySelector
{
    public class QuantitySelectorViewComponent : ViewComponent
    {
        private readonly IApiService _apiService;
        public QuantitySelectorViewComponent(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int productId)
        {
            var userId = HttpContext.Session.GetInt32("userId");
            var orderId = HttpContext.Session.GetInt32("orderId");
            var tableId = HttpContext.Session.GetInt32("tableId");
            if (userId != null && orderId != null && tableId != null)
            {
                var orderDetail = await _apiService.CheckOrderDetailExist(userId ?? 0 , orderId ?? 0, tableId ?? 0, productId);
                if (orderDetail.IsSussess)
                {
                    QuantitySelectorViewModel viewModel = new QuantitySelectorViewModel
                    {
                        OrderId = orderDetail.Data?.DhId ?? 0,
                        ProductId = productId,
                        Quantity = orderDetail.Data?.SoLuong ?? 0,
                    };
                    return View("QuantityView", viewModel);
                }
            } 
            return View("QuantityViewOrderNull" , productId);
        }
    }
}
