using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STS.Core.Entities
{
    public class Definition
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}