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
        Task<ResponseModel<Order>> CheckOrderOrInitOrder(int userId, int tableId);
        Task<ResponseModel<RequestOrderDetail>> AddOrderDetailOnOrder(RequestOrderDetail requestOrderDetail);
        Task<ResponseModel<RequestOrderDetail>> CheckOrderDetailExist(int userId, int orderId, int tableId , int productId);
        Task<ResponseModel<RequestOrderDetail>> OrderDetailMore(int userId, int orderId, int productId, int quantity);
        Task<ResponseModel<RequestOrderDetail>> OrderDetailReduce(int userId, int orderId, int productId, int quantity);

        Task<ResponseModel<bool>> DeleteOrderDetail(int orderId, int productId);
        Task<ResponseModel<int>> CountOrderDetailInOrder(int orderId);
        Task<ResponseModel<Product>> SearchProductByName(string productName);
        Task<ResponseModel<Category>> GetAllCategorys();
    }
}
