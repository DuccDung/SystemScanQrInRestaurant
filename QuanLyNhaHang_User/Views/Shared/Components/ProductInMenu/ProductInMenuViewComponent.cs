using Microsoft.AspNetCore.Mvc;
using QuanLyNhaHang_User.Models;
using QuanLyNhaHang_User.Sevices;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuanLyNhaHang_User.Views.Shared.Components.ProductInMenu
{
    public class ProductInMenuViewComponent : ViewComponent
    {
        private readonly IApiService _apiService;

        public ProductInMenuViewComponent(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ProductInMenuViewModel> dataModel = new List<ProductInMenuViewModel>();
            var categoriesResult = await _apiService.GetAllCategorys();
            if (categoriesResult.IsSussess)
            {
                foreach (var category in categoriesResult.DataList)
                {
                    ProductInMenuViewModel temp = new ProductInMenuViewModel();
                    temp.Category = category;

                    var productsResult = await _apiService.GetAllProductByCategoryId(category.CateId);
                    if (productsResult.IsSussess && productsResult.DataList != null)
                    {
                        temp.Products = productsResult.DataList;
                    }
                    dataModel.Add(temp);
                }
            }
            return View("RenderProductInMenu", dataModel);
        }
    }
}
