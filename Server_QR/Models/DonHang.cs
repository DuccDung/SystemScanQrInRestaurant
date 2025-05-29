﻿using System;
using System.Collections.Generic;

namespace Server_QR.Models;

public partial class DonHang
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

    public virtual Ban? Ban { get; set; }

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    public virtual KhachHang? Kh { get; set; }

    public virtual KhuyenMai? Km { get; set; }

    public virtual NhanVien? Nv { get; set; }
}
