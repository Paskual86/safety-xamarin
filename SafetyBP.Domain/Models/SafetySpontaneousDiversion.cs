﻿using SafetyBP.Domain.Enums;

namespace SafetyBP.Domain.Models
{
    public class SafetySpontaneousDiversion
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public int SectorId { get; set; }
        public SpontaneousDiversionRisk Risk { get; set; }
        public string Photo { get; set; }
        public string Comment { get; set; }
        public bool Synchronized { get; set; }

        public virtual SafetySector Sector { get; set; }
        public SafetySpontaneousDiversion()
        {
            Synchronized = false;
            Reason = string.Empty;
            Photo = string.Empty;
            Comment = string.Empty;
        }
    }
}
