using Microsoft.AspNetCore.Mvc;
using QuanLyNhaHang_User.Sevices;

namespace QuanLyNhaHang_User.Views.Shared.Components.FoodterMenu
{
    public class FoodterMenuViewComponent : ViewComponent
    {
        private readonly IApiService _apiService;
        public FoodterMenuViewComponent(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var count =await _apiService.CountOrderDetailInOrder(HttpContext.Session.GetInt32("orderId") ?? 0);
            if(count.Data > 0)
            {
                return View("FoodterMenu", count.Data);
            }
            else
            {
                return View("NullView");
            }
        }
    }
}
