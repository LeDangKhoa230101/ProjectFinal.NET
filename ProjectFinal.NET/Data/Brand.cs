using System;
using System.Collections.Generic;

namespace ProjectFinal.NET.Data;

public partial class Brand
{
    public int IdBrand { get; set; }

    public string BrandName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
