using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.DataTransferObjects.DTOs.NGO
{
    public class NgoDTO
    {
        public int? Id { get; set; }
        [StringLength(60, ErrorMessage = "Name cannot exceed 60 characters.")]
        public string Name { get; set; } // NVARCHAR
    }
}
