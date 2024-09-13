using STS.DataTransferObjects.DTOs.Shared.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STS.DataTransferObjects.DTOs.CartItem
{
    public class AddUpdateCartItemDTO 
    {
        public int? Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }

    }
}
