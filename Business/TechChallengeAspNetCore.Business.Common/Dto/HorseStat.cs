using Eml.EntityBaseClasses;

namespace TechChallengeAspNetCore.Business.Common.Dto
{
    public class HorseStat : EntityBaseInt
    {
        public int RaceId { get; set; }

        public string Name { get; set; }

        public double Odds { get; set; }

        public int BetCount { get; set; }

        public double WinAmount { get; set; }
    }
}

