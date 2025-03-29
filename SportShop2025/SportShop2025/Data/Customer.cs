using System;
using System.Collections.Generic;

namespace SportShop2025.Data;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Address { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
