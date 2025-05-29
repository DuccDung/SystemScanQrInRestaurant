using System;
using System.Collections.Generic;

namespace Server_QR.Models;

public partial class NvPq
{
    public int NvPqId { get; set; }

    public int? NvId { get; set; }

    public int? PqId { get; set; }

    public string? MoTa { get; set; }

    public virtual NhanVien? Nv { get; set; }

    public virtual PhanQuyen? Pq { get; set; }
}
