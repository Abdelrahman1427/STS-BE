﻿using System.ComponentModel.DataAnnotations;

namespace PathFinder.Core.Entities
{
    public class Assessment : Definition
    {
        [Key]
        public int Id { get; set; }    
        [StringLength(60,ErrorMessage = "Name cannot exceed 60 characters")]
        public string ArName { get; set; }
        [StringLength(60,ErrorMessage ="Name cannot exceed 60 characters")]
        public string EnName { get; set; }
        public DateTime StartDate  { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<PrivateSector> PrivateSectors { get; set; }
    }
}