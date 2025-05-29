using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using QuanLyNhaHang_User.Sevices;

namespace QuanLyNhaHang_User.Views.Shared.Components.ProductCondition
{
    public class ProductConditionViewComponent:ViewComponent
    {
        private IApiService _apiService;
        public ProductConditionViewComponent(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int ProductID) 
        {
            var conditions = await _apiService.GetProductConditionByProductId(ProductID);
            if (!conditions.IsSussess || conditions.DataList == null || !conditions.DataList.Any())
            {
                return View("NullView");
            }
            return View("CheckboxCondition" , conditions.DataList);
        }
    }
}
