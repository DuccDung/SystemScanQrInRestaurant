using System;
using System.Collections.Generic;

namespace Server_QR.Models;

public partial class ProductCondition
{
    public int ProductConditionId { get; set; }

    public int? ProductId { get; set; }

    public string? Condition { get; set; }

    public virtual Product? Product { get; set; }
}
