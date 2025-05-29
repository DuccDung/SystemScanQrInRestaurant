namespace QuanLyNhaHang_User.Models
{
    public class ProductOrder
    {
        public int ProductID { get; set; }
        public int SoLuong { get; set; }
        public string? GhiChu { get; set; }
        public List<string> Conditions { get; set; } = new();
    }

}
