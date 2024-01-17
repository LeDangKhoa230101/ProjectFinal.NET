using System;
using System.Collections.Generic;

namespace ProjectFinal.NET.Data;

public partial class Category
{
    public int IdCate { get; set; }

    public string CateName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
