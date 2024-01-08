using System;
using System.Collections.Generic;

namespace ProjectFinal.NET.Data;

public partial class Product
{
    public int IdProduct { get; set; }

    public string? NameProduct { get; set; }

    public string? Image { get; set; }

    public int? PriceNew { get; set; }

    public int? PriceOld { get; set; }

    public int? Quantity { get; set; }

    public string? Description { get; set; }

    public int? IdBrand { get; set; }

    public int? IdCate { get; set; }

    public virtual Brand? IdBrandNavigation { get; set; }

    public virtual Category? IdCateNavigation { get; set; }
}
