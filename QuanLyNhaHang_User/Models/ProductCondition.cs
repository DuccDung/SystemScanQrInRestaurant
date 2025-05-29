using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Server_QR.Models;

public class ProductCondition
{
    [JsonPropertyName("productConditionId")]
    public int ProductConditionId { get; set; }
    [JsonPropertyName("productId")]
    public int? ProductId { get; set; }
    [JsonPropertyName("condition")]
    public string? Condition { get; set; }
}
