using Eml.EntityBaseClasses;
using System;
using System.Collections.Generic;

namespace TechChallengeAspNetCore.Business.Common.Dto
{
    public class RaceStat : EntityBaseInt
    {
        public string Name { get; set; }

        public string Status { get; set; }

        public DateTime Start { get; set; }

        public double RaceTotalAmount { get; set; }

        public List<HorseStat> HorseStats { get; set; } = new List<HorseStat>();
    }
}

