using System;
using System.Collections.Generic;

namespace Server_QR.Models;

public partial class Category
{
    public int CateId { get; set; }

    public string? TenLoaiSanPham { get; set; }

    public string? MoTa { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
