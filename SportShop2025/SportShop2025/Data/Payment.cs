using System;
using System.Collections.Generic;

namespace SportShop2025.Data;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int? OrderId { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public DateTime? PaymentDate { get; set; }

    public decimal Amount { get; set; }

    public virtual Order? Order { get; set; }
}
