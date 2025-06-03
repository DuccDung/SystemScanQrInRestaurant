using Azure;
using Server_QR.Models;

namespace Server_QR.Services
{
    public interface IApiService
    {
        Task<ResponseModel<KhachHang>> GetUserInformation(string userId);
        Task<ResponseModel<bool>> CheckUserAlreadyExists(int userId);
        Task<ResponseModel<KhachHang>> CreateUser(string userName);
        Task<ResponseModel<Product>> GetProductById(int productId);
        Task<ResponseModel<ProductCondition>> GetProductConditionByProductId(int productId);
        Task<ResponseModel<DonHang>> InitializeOrder(int userId, int tableId);
        Task<ResponseModel<DonHang>> CheckOrInitOrder(int userId, int tableId);
        Task<ResponseModel<ChiTietHoaDon>> AddOrderDetailInOrder(RequestOrderDetail requestOrderDetail);
        Task<ResponseModel<RequestOrderDetail>> CheckOrderDetailExist(int userId , int orderId , int tableId , int productId);
        Task<ResponseModel<RequestOrderDetail>> OrderDetailMore(int userId, int orderId , int productId , int quantiy);
        Task<ResponseModel<RequestOrderDetail>> OrderDetailReduce(int userId, int orderId , int productId , int quantiy);
        Task<ResponseModel<bool>> DeleteOrderDetail(int orderId, int productId);
        Task<ResponseModel<int>> CountOrderDetailInOrder(int orderId);
        Task<ResponseModel<Product>> SearchProductByName(string productName);
        Task<ResponseModel<Category>> GetAllCategory();
        Task<ResponseModel<Product>> GetAllProductByCategoryId(int categoryId);
    }
}
