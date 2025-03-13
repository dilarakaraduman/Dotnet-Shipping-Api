﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Dtos
{
    public class OrderDto
    {
            public int OrderId { get; set; }
            public int OrderDesi { get; set; }
            public decimal OrderCarrierCost { get; set; }
            public string CarrierName { get; set; }
    }

}
