using STS.DataTransferObjects.DTOs.Shared.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STS.DataTransferObjects.DTOs.CartItem
{
    public class GetPageCartItemDTO 
    {
        public int? Id { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }

    }
}
