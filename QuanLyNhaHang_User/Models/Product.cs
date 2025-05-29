using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QuanLyNhaHang_User.Models
{
    public class Product
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }

        [JsonPropertyName("cateId")]
        public int CateId { get; set; }

        [JsonPropertyName("tenSanPham")]
        public string? TenSanPham { get; set; }

        [JsonPropertyName("moTa")]
        public string? MoTa { get; set; }

        [JsonPropertyName("giaTien")]
        public float GiaTien { get; set; }

        [JsonPropertyName("pathPhoto")]
        public string? PathPhoto { get; set; }

        [JsonPropertyName("cate")]
        public object Cate { get; set; }   // Cate là null nên để object, hoặc bạn có thể tạo model riêng nếu cần.

        [JsonPropertyName("chiTietHoaDons")]
        public List<object> ChiTietHoaDons { get; set; }

        [JsonPropertyName("congThucs")]
        public List<object> CongThucs { get; set; }

        [JsonPropertyName("productConditions")]
        public List<object> ProductConditions { get; set; }
    }

}
