﻿using System.ComponentModel.DataAnnotations;

namespace PathFinder.Core.Entities
{
    public class Engagement : Definition
    {
        [Key]
        public int Id { get; set; }
        [StringLength(60, ErrorMessage = "Ar Name cannot exceed 60 characters.")]
        public string ArName { get; set; }
        [StringLength(60, ErrorMessage = "En Name cannot exceed 60 characters.")]
        public string EnName { get; set; }
        public DateTime OccurDate { get; set; }
        public ICollection<FinancialServiceProvider> FinancialServiceProviders { get; set; }
        public ICollection<PrivateSector> PrivateSectors { get; set; }
        public ICollection<Beneficiarie> Beneficiaries { get; set; }
    }
}