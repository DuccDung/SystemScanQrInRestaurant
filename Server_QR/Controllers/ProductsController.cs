using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server_QR.Models;
using Server_QR.Services;

namespace Server_QR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly QlnhaHangBtlContext _context;
        private readonly IApiService _apiService;
        public ProductsController(QlnhaHangBtlContext context, IApiService apiService)
        {
            _context = context;
            _apiService = apiService;
        }

        [Route("GetAllProducts")]
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _context.Products.ToList();
            if (products == null || !products.Any())
            {
                return NotFound("No products found.");
            }
            return Ok(products);
        }
        [Route("GetAllMemberCate")]
        [HttpGet]
        public IActionResult GetAllMemberCate()
        {
            var memberCates = _context.Categories.ToList();

            if (memberCates == null || !memberCates.Any())
            {
                return NotFound("No member categories found.");
            }
            return Ok(memberCates);
        }
        [Route("GetAllProductsByCateId")]
        [HttpGet]
        public IActionResult GetAllProductsByCate(int id)
        {
            var products = _context.Products.Where(p => p.CateId == id).ToList();
            if (products == null || !products.Any())
            {
                return NotFound("No products found for the specified category.");
            }
            return Ok(products);
        }
        [Route("GetProductById")]
        [HttpGet]
        public async Task<IActionResult> GetProductById(int productId)
        {
            return Ok(await _apiService.GetProductById(productId));
        }

        [Route("GetProductConditionByProductId")]
        [HttpGet]
        public async Task<IActionResult> GetProductConditionByProductId(int productId)
        {
            return Ok(await _apiService.GetProductConditionByProductId(productId));
        }
        [Route("SearchProductByName")]
        [HttpGet]
        public async Task<IActionResult> SearchProductByName(string productName)
        {
            var response = await _apiService.SearchProductByName(productName);
            return Ok(response);
        }
    }
}
