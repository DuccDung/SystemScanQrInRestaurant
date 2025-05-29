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
            var user =await _context.KhachHangs.FirstOrDefaultAsync(u => u.KhId == int.Parse(userId));
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
    }
}

