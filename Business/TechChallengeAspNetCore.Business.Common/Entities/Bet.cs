using System.ComponentModel.DataAnnotations;
using Eml.EntityBaseClasses;

namespace TechChallengeAspNetCore.Business.Common.Entities
{
    public class Bet : EntityBaseSoftDeleteInt
    {
        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Required]
        [Display(Name = "Horse")]
        public int HorseId { get; set; }

        [Required]
        [Display(Name = "Race")]
        public int RaceId { get; set; }

        public double Stake { get; set; }
    }
}
