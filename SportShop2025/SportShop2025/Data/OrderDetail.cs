using System;
using System.Collections.Generic;

namespace SportShop2025.Data;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal? SubTotal { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}
