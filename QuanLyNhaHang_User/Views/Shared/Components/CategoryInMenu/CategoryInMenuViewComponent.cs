using Microsoft.AspNetCore.Mvc;
using QuanLyNhaHang_User.Models;
using System.Text.Json;

namespace QuanLyNhaHang_User.Views.Shared.Components.CategoryInMenu
{
    public class CategoryInMenuViewComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;
        public CategoryInMenuViewComponent(IHttpClientFactory httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClient.CreateClient();
            var baseUrl = _configuration["ApiSettings:BaseUrl"];
            var response = await client.GetAsync($"{baseUrl}api/Products/GetAllMemberCate");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var categories = JsonSerializer.Deserialize<List<Category>>(jsonString);

                return View("RenderCategoryInMenu", categories);

            }
            else
            {
                return View("Error");
            }
        }

    }
}
