using QuanLyNhaHang_User.Models;
using Server_QR.Models;

namespace QuanLyNhaHang_User.Sevices
{
    public interface IApiService
    {
        Task<ResponseModel<User>> GetUserInformation(string userId);
        Task<ResponseModel<User>> PostUserCreate(string nameUser);
        Task<ResponseModel<Product>> GetProductById(int productId);
        Task<ResponseModel<ProductCondition>> GetProductConditionByProductId(int productId);
        Task<ResponseModel<Order>> InitializeOrder(int userId, int tableId);
        Task<ResponseModel<RequestOrderDetail>> AddOrderDetailOnOrder(RequestOrderDetail requestOrderDetail);
    }
}
