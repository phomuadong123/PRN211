using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Cart
{
    public int Quantity { get; set; }

    public decimal Price { get; set; }
}
