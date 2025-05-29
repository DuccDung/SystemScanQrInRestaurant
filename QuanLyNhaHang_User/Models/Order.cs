namespace QuanLyNhaHang_User.Models
{
    public class Order
    {
        public int DhId { get; set; }

        public int? KhId { get; set; }

        public int? BanId { get; set; }

        public int? KmId { get; set; }

        public DateTime? GioVao { get; set; }

        public DateTime? GioRa { get; set; }

        public decimal? TongTien { get; set; }

        public int? NvId { get; set; }

        public string? GhiChu { get; set; }

        public bool? TrangThai { get; set; }

        public bool? VanChuyen { get; set; }

    }
}
