using STS.DataTransferObjects.DTOs.Shared.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STS.DataTransferObjects.DTOs.Product
{
    public class AddUpdateProductDTO : DefinitionDTO
    {
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? PictureUrl { get; set; }
        public int CategoryId { get; set; }
    }
}
