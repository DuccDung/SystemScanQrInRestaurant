using Microsoft.EntityFrameworkCore;
using Server_QR.Models;

namespace Server_QR.Services
{
    public class ApiService : IApiService
    {
        private readonly QlnhaHangBtlContext _context;
        public ApiService(QlnhaHangBtlContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<ChiTietHoaDon>> AddOrderDetailInOrder(RequestOrderDetail requestOrderDetail)
        {
            ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon
            {
                DhId = requestOrderDetail.DhId,
                ProductId = requestOrderDetail.ProductId,
                SoLuong = requestOrderDetail.SoLuong,
                ThanhTien = requestOrderDetail.ThanhTien,
                Ghichu = requestOrderDetail.Ghichu
            };
            await _context.ChiTietHoaDons.AddAsync(chiTietHoaDon);
            await _context.SaveChangesAsync();
            ResponseModel<ChiTietHoaDon> responseModel = new ResponseModel<ChiTietHoaDon>
            {
                IsSussess = true,
                Message = "Order detail added successfully.",
                Data = chiTietHoaDon
            };
            return responseModel;
        }

        public async Task<ResponseModel<DonHang>> CheckOrInitOrder(int userId, int tableId)
        {
            var order =await _context.DonHangs
                .Where(o => o.KhId == userId && o.BanId == tableId && o.TrangThai == false)
                .FirstOrDefaultAsync();
            if (order != null)
            {
                ResponseModel<DonHang> responseModel = new ResponseModel<DonHang>
                {
                    IsSussess = true,
                    Message = "Order exists.",
                    Data = order
                };
                return responseModel;
            }
            else
            {
                DonHang donHang = new DonHang
                {
                    KhId = userId,
                    BanId = tableId,
                    GioVao = DateTime.Now,
                    TrangThai = false
                };
                await _context.DonHangs.AddAsync(donHang);
                await _context.SaveChangesAsync();

                ResponseModel<DonHang> responseModel = new ResponseModel<DonHang>
                {
                    IsSussess = false,
                    Message = "Order initialized successfully.",
                    Data = donHang
                };
                return responseModel;
            }
        }

        public async Task<ResponseModel<RequestOrderDetail>> CheckOrderDetailExist(int userId, int orderId, int tableId , int productId)
        {
            // Check if the order detail exists for the given userId, orderId, and tableId
            var orderDetail = await _context.ChiTietHoaDons
                 .Where(c => c.DhId == orderId)
                 .Include(c => c.Dh).Where(c => c.Dh.BanId == tableId && c.Dh.KhId == userId)
                 .Include(c => c.Product).Where(c => c.ProductId == productId)
                 .FirstOrDefaultAsync();
            if(orderDetail != null)
            {
                ResponseModel<RequestOrderDetail> responseModel = new ResponseModel<RequestOrderDetail>()
                {
                    IsSussess = true,
                    Message = "Order detail exists.",
                    Data = new RequestOrderDetail
                    {
                        DhId = orderDetail.DhId,
                        ProductId = orderDetail.ProductId,
                        SoLuong = orderDetail.SoLuong,
                        ThanhTien = orderDetail.ThanhTien,
                        Ghichu = orderDetail.Ghichu
                    }
                };
                return responseModel;
            }
            else
            {
                ResponseModel<RequestOrderDetail> responseModel = new ResponseModel<RequestOrderDetail>()
                {
                    IsSussess = false,
                    Message = "Order detail does not exist.",
                    Data = null
                };
                return responseModel;
            }
        }

        public async Task<ResponseModel<bool>> CheckUserAlreadyExists(int userId)
        {
            var user = await _context.KhachHangs.FirstOrDefaultAsync(u => u.KhId == userId);
            ResponseModel<bool> responseModel = new ResponseModel<bool>();

            if (user == null)
            {
                responseModel.IsSussess = false;
                responseModel.Message = "User not found.";
            }
            else
            {
                responseModel.IsSussess = true;
                responseModel.Message = "User exists.";
            }
            return responseModel;
        }

        public async Task<ResponseModel<KhachHang>> CreateUser(string userName)
        {
            KhachHang khachHang = new KhachHang
            {
                TenKhachHang = userName,
                TaiKhoan = "KhachHang",
                MatKhau = "null",
            };

            await _context.KhachHangs.AddAsync(khachHang);
            await _context.SaveChangesAsync();

            ResponseModel<KhachHang> responseModel = new ResponseModel<KhachHang>
            {
                IsSussess = true,
                Message = "User created successfully.",
                Data = khachHang
            };

            return responseModel;
        }

        public async Task<ResponseModel<Product>> GetProductById(int productId)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == productId);
            ResponseModel<Product> responseModel = new ResponseModel<Product>();
            if (product == null)
            {
                responseModel.IsSussess = false;
                responseModel.Message = "Product not found.";
            }
            else
            {
                responseModel.IsSussess = true;
                responseModel.Message = "Product retrieved successfully.";
                responseModel.Data = product;
            }
            return responseModel;
        }

        public async Task<ResponseModel<ProductCondition>> GetProductConditionByProductId(int productId)
        {
            var productCondition = await _context.ProductConditions
                .Where(pc => pc.ProductId == productId).ToListAsync();

            ResponseModel<ProductCondition> responseModel = new ResponseModel<ProductCondition>();
            if (productCondition == null || !productCondition.Any())
            {
                responseModel.IsSussess = false;
                responseModel.Message = "Product condition not found.";
            }
            else
            {
                responseModel.IsSussess = true;
                responseModel.Message = "Product condition retrieved successfully.";
                responseModel.DataList = productCondition; // Fix: Assign to Data instead of DataList
            }
            return responseModel;
        }

        public async Task<ResponseModel<KhachHang>> GetUserInformation(string userId)
        {
            var user = await _context.KhachHangs.FirstOrDefaultAsync(u => u.KhId == int.Parse(userId));
            ResponseModel<KhachHang> responseModel = new ResponseModel<KhachHang>();
            if (user == null)
            {
                responseModel.IsSussess = false;
                responseModel.Message = "User not found.";
            }
            else
            {
                responseModel.IsSussess = true;
                responseModel.Message = "User information retrieved successfully.";
                responseModel.Data = user;
            }
            return responseModel;
        }

        public async Task<ResponseModel<DonHang>> InitializeOrder(int userId, int tableId)
        {
            DonHang donHang = new DonHang
            {
                KhId = userId,
                BanId = tableId,
                GioVao = DateTime.Now,
                TrangThai = false
            };
            // Optional: You can set other properties like KmId, NvId, etc. if needed
            await _context.DonHangs.AddAsync(donHang);
            await _context.SaveChangesAsync();

            ResponseModel<DonHang> responseModel = new ResponseModel<DonHang>
            {
                IsSussess = true,
                Message = "Order initialized successfully.",
                Data = donHang
            };
            return responseModel;
        }

        public async Task<ResponseModel<RequestOrderDetail>> OrderDetailMore(int userId, int orderId, int productId, int quantiy)
        {
            var orderDetail = await _context.ChiTietHoaDons.Where(c => c.DhId == orderId && c.ProductId == productId).FirstOrDefaultAsync();
            var product = await _context.Products.Where(p => p.ProductId == productId).FirstOrDefaultAsync();
            if (orderDetail != null && product != null)
            {
                orderDetail.SoLuong += quantiy;
                orderDetail.ThanhTien = orderDetail.SoLuong * product.GiaTien;
                _context.ChiTietHoaDons.Update(orderDetail);
            }
            else
            {
                ChiTietHoaDon newOrderDetail = new ChiTietHoaDon
                {
                    DhId = orderId,
                    ProductId = productId,
                    SoLuong = quantiy,
                    ThanhTien = quantiy * (await _context.Products.Where(p => p.ProductId == productId).Select(p => p.GiaTien).FirstOrDefaultAsync()),
                };
                await _context.ChiTietHoaDons.AddAsync(newOrderDetail);
            }
            await _context.SaveChangesAsync();

            ResponseModel<RequestOrderDetail> requestOrderDetailResponse = new ResponseModel<RequestOrderDetail>
            {
                IsSussess = true,
                Message = "Order detail updated successfully.",
                Data = new RequestOrderDetail
                {
                    DhId = orderId,
                    ProductId = productId,
                    SoLuong = orderDetail?.SoLuong ?? quantiy,
                    ThanhTien = orderDetail?.ThanhTien ?? (quantiy * (await _context.Products.Where(p => p.ProductId == productId).Select(p => p.GiaTien).FirstOrDefaultAsync())),
                    Ghichu = orderDetail?.Ghichu
                }
            };
            return requestOrderDetailResponse;
        }

        public async Task<ResponseModel<RequestOrderDetail>> OrderDetailReduce(int userId, int orderId, int productId, int quantiy)
        {
            var orderDetail = await _context.ChiTietHoaDons.Where(c => c.DhId == orderId && c.ProductId == productId).FirstOrDefaultAsync();
            var product = await _context.Products.Where(p => p.ProductId == productId).FirstOrDefaultAsync();
            if (orderDetail != null && product != null && orderDetail.SoLuong > 0)
            {
                orderDetail.SoLuong -= quantiy;
                orderDetail.ThanhTien = orderDetail.SoLuong * product.GiaTien;
                _context.ChiTietHoaDons.Update(orderDetail);
            }
            await _context.SaveChangesAsync();
            ResponseModel<RequestOrderDetail> requestOrderDetailResponse = new ResponseModel<RequestOrderDetail>
            {
                IsSussess = true,
                Message = "Order detail updated successfully.",
                Data = new RequestOrderDetail
                {
                    DhId = orderId,
                    ProductId = productId,
                    SoLuong = orderDetail?.SoLuong,
                    ThanhTien = orderDetail?.ThanhTien,
                    Ghichu = orderDetail?.Ghichu
                }
            };
            return requestOrderDetailResponse;
        }
    }
}

