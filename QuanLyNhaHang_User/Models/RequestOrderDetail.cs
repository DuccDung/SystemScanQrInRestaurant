namespace QuanLyNhaHang_User.Models
{
    public class RequestOrderDetail
    {
        public int CthdId { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien { get; set; }
        public int DhId { get; set; }
        public int ProductId { get; set; }
        public string? Ghichu { get; set; }
        public int? GiamGia { get; set; }
    }
}
