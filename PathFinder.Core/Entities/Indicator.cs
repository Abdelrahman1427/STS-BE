using PathFinder.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Indicator
{
    public int Id { get; set; }
    [StringLength(60, ErrorMessage = "Code cannot exceed 60 characters.")]
    public string Code { get; set; } // Required, max length 60
    [StringLength(60, ErrorMessage = "Ar Name cannot exceed 60 characters.")]
    public string ArName { get; set; } // Required, max length 60
    [StringLength(60, ErrorMessage = "En Name cannot exceed 60 characters.")]
    public string EnName { get; set; } // Required, max length 60
    [StringLength(250, ErrorMessage = "Description cannot exceed 250 characters.")]
    public string? Description { get; set; } // Optional, max length 250
    public int IndicatorTypeId { get; set; } // Required
    public double? Value { get; set; }
    public double? Target { get; set; }
    public int ObjectiveId { get; set; } // Required
    [ForeignKey("ObjectiveId")]
    public Objective Objective { get; set; } // Navigation property
    public ICollection<IndicatorTransaction> IndicatorsTransactions { get; set; }
}