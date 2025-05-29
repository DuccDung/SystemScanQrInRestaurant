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
        Task<ResponseModel<ChiTietHoaDon>> AddOrderDetailInOrder(RequestOrderDetail requestOrderDetail);
    }
}
