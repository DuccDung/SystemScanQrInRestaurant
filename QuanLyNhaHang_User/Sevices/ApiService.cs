
using Microsoft.AspNetCore.Http.HttpResults;
using QuanLyNhaHang_User.Models;
using Server_QR.Models;
using System.Net.Http;

namespace QuanLyNhaHang_User.Sevices
{
    public class ApiService : IApiService
    {
        private readonly HttpClient client;

        public ApiService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<ResponseModel<RequestOrderDetail>> AddOrderDetailOnOrder(RequestOrderDetail requestOrderDetail)
        {
            var response = await client.PostAsync("Orders/AddOrderDetailInOrder" ,JsonContent.Create(requestOrderDetail));
            if (response.IsSuccessStatusCode)
            {
                var orderDetail = await response.Content.ReadAsAsync<ResponseModel<RequestOrderDetail>>();
                return orderDetail;
            }


            return new ResponseModel<RequestOrderDetail>
            {
                IsSussess = false,
                Message = "Failed add order detail on main order."
            };
        }

        public async Task<ResponseModel<RequestOrderDetail>> CheckOrderDetailExist(int userId, int orderId, int tableId, int productId)
        {
            var reponse =await client.GetAsync($"Orders/CheckOrderDetailExist?userId={userId}&orderId={orderId}&tableId={tableId}&productId={productId}");
            if (reponse.IsSuccessStatusCode)
            {
                var orderDetail =await reponse.Content.ReadAsAsync<ResponseModel<RequestOrderDetail>>();
                if (orderDetail.IsSussess)
                {
                    return orderDetail;
                }
            }
            return new ResponseModel<RequestOrderDetail>
            {
                IsSussess = false,
                Message = "Failed, product no exist."
            };
        }

        public async Task<ResponseModel<Order>> CheckOrderOrInitOrder(int userId, int tableId)
        {
            var response =await client.GetAsync($"Orders/CheckOrderExistOrInitOrder?userId={userId}&tableId={tableId}");
            if (response.IsSuccessStatusCode)
            {
                var order = await response.Content.ReadAsAsync<ResponseModel<Order>>();
                return order;
            }
            return new ResponseModel<Order>
            {
                IsSussess = false,
                Message = "404."
            };
        }

        public async Task<ResponseModel<Product>> GetProductById(int productId)
        {
            // This method is not implemented yet. api/Products/GetProductById?productId=1
            var response = await client.GetAsync($"Products/GetProductById?productId={productId}");
            if (response.IsSuccessStatusCode)
            {
                var product = await response.Content.ReadAsAsync<ResponseModel<Product>>();
                if (product.Data != null)
                {
                    return product;
                }
            }
            return new ResponseModel<Product>
            {
                IsSussess = false,
                Message = "Failed to retrieve product information."
            };
        }

        public async Task<ResponseModel<ProductCondition>> GetProductConditionByProductId(int productId)
        {
            var response =await client.GetAsync($"Products/GetProductConditionByProductId?productId={productId}");
            if (response.IsSuccessStatusCode)
            {
                var conditions =await response.Content.ReadAsAsync<ResponseModel<ProductCondition>>();
                if (conditions.DataList != null)
                {
                    return conditions;
                }
            }
            return new ResponseModel<ProductCondition>
            {
                IsSussess = false,
                Message = "Failed to retrieve product conditions."
            };
        }

        public async Task<ResponseModel<User>> GetUserInformation(string userId)
        {
            var response = await client.GetAsync($"Users/GetUserInformation?userId={userId}");
            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadAsAsync<ResponseModel<User>>();
                return user;
            }
            else
            {
                return new ResponseModel<User>
                {
                    IsSussess = false,
                    Message = "Failed to retrieve user information."
                };
            }
        }

        public async Task<ResponseModel<Order>> InitializeOrder(int userId, int tableId)
        {
            var response = await client.GetAsync($"Orders/InitializeOrder?userId={userId}&tableId={tableId}");
            if (response.IsSuccessStatusCode)
            {
                var orderResponse = await response.Content.ReadAsAsync<ResponseModel<Order>>();
                return orderResponse;
            }
            return new ResponseModel<Order>
            {
                IsSussess = false,
                Message = "Failed to initialize order."
            };
        }

        public async Task<ResponseModel<RequestOrderDetail>> OrderDetailMore(int userId, int orderId, int productId, int quantity)
        {
            var response = await client.GetAsync($"Orders/OrderDetailMore?userId={userId}&orderId={orderId}&productId={productId}&quantiy={quantity}");
            if (response.IsSuccessStatusCode)
            {
                var orderDetail = await response.Content.ReadAsAsync<ResponseModel<RequestOrderDetail>>();
                return orderDetail;
                
            }
            else
            {
                return new ResponseModel<RequestOrderDetail>
                {
                    IsSussess = false,
                    Message = "Fail, no more quantity in order detail! "
                };
            }
        }

        public async Task<ResponseModel<RequestOrderDetail>> OrderDetailReduce(int userId, int orderId, int productId, int quantity)
        {
            var response = await client.GetAsync($"Orders/OrderDetailReduce?userId={userId}&orderId={orderId}&productId={productId}&quantiy={quantity}");
            if (response.IsSuccessStatusCode)
            {
                var orderDetail =await response.Content.ReadAsAsync<ResponseModel<RequestOrderDetail>>();
                return orderDetail;
            }
            else
            {
                return new ResponseModel<RequestOrderDetail>
                {
                    IsSussess = false,
                    Message = "Fail, no reduce quantity in order detail!"
                };
            }

        }

        public async Task<ResponseModel<User>> PostUserCreate(string nameUser)
        {
            var response = await client.GetAsync($"Users/InitUser?userName={nameUser}");
            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadAsAsync<ResponseModel<User>>();
                return user;
            }
            else
            {
                return new ResponseModel<User>
                {
                    IsSussess = false,
                    Message = "Failed to create user."
                };
            }
        }
    }
}
