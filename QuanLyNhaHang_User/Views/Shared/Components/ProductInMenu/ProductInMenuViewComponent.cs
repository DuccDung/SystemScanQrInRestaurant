using Microsoft.AspNetCore.Mvc;
using QuanLyNhaHang_User.Models;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuanLyNhaHang_User.Views.Shared.Components.ProductInMenu
{
    public class ProductInMenuViewComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;
        public ProductInMenuViewComponent(IHttpClientFactory httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public class DataModel
        {
            private readonly IHttpClientFactory _httpClient;
            private readonly IConfiguration _configuration;
            public DataModel(IHttpClientFactory httpClient, IConfiguration configuration)
            {
                _httpClient = httpClient;
                _configuration = configuration;
            }
            public List<Category> GetDataCategory()
            {
                var client = _httpClient.CreateClient();
                var baseUrl = _configuration["ApiSettings:BaseUrl"];
                var response = client.GetAsync($"{baseUrl}api/Products/GetAllMemberCate").Result; // dùng .Result để đồng bộ

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result; // dùng .Result để đồng bộ
                    var categories = JsonSerializer.Deserialize<List<Category>>(jsonString);
                    return categories;
                }
                else
                {
                    return null;
                }
            }

            public List<Product> GetDataProduct(int CateId)
            {
                var client = _httpClient.CreateClient();
                var baseUrl = _configuration["ApiSettings:BaseUrl"];
                var response = client.GetAsync($"{baseUrl}api/Products/GetAllProductsByCateId?id={CateId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var Products = JsonSerializer.Deserialize<List<Product>>(jsonString);
                    return Products;
                }
                else
                {
                    return null;
                }
            }
        }

        public IViewComponentResult Invoke()
        {
            DataModel dataModel = new DataModel(_httpClient, _configuration);
            return View("RenderProductInMenu", dataModel);
        }
    }
}
