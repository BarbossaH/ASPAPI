using System;
using System.Collections.Generic;

namespace ASPAPI.Models;

public partial class Customer
{
    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public decimal? CreditLimit { get; set; }

    public bool? IsActive { get; set; }

    public int? TaxCode { get; set; }
}
