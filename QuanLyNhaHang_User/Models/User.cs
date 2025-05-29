using System;
using System.Collections.Generic;

namespace Server_QR.Models;

public partial class User
{
    public int KhId { get; set; }

    public string TenKhachHang { get; set; } = null!;

    public string? DiaChi { get; set; }

    public string? SoDienThoai { get; set; }

    public string TaiKhoan { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string? PathPhoto { get; set; }

}
