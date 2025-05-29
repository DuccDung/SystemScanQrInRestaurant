using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QuanLyNhaHang_User.Models;


public class Category
{
    [JsonPropertyName("cateId")]
    public int CateId { get; set; }

    [JsonPropertyName("tenLoaiSanPham")]
    public string TenLoaiSanPham { get; set; }

    [JsonPropertyName("moTa")]
    public string MoTa { get; set; }

    [JsonPropertyName("products")]
    public List<object> Products { get; set; }
}
