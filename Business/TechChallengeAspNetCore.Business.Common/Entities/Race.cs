using System.Collections.Generic;
using System;
using Eml.EntityBaseClasses;

namespace TechChallengeAspNetCore.Business.Common.Entities
{
    public class Race : EntityBaseSoftDeleteInt
    {
        public string Name { get; set; }

        public string Status { get; set; }

        public DateTime Start { get; set; }

        public virtual List<Horse> Horses { get; set; }
    }
}
