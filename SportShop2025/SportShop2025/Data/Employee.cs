using System;
using System.Collections.Generic;

namespace SportShop2025.Data;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateTime? HireDate { get; set; }

    public string Position { get; set; } = null!;

    public decimal Salary { get; set; }
}
