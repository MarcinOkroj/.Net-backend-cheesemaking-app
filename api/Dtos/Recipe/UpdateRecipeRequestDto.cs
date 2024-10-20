using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Enums;

namespace api.Dtos.Recipe
{
    public class UpdateRecipeRequestDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot be over 50 characters")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(50, ErrorMessage = "Style cannot be over 50 characters")]
        public string Style { get; set; } = string.Empty;
        [Required]
        [MaxLength(300, ErrorMessage = "Name cannot be over 300 characters")]
        public string Description { get; set; } = string.Empty;
        [Required]
        public ComplexityLevel Complexity { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Bacterias cannot be over 50 characters")]
        public string Bacterias { get; set; } = string.Empty;
        [Required]
        [Range(1, 200)]
        public decimal ExpectedYield { get; set; }
        [Required]
        [Range(1, 400)]
        public decimal MilkVolume { get; set; }
        [Required]
        [Range(1, 100)]
        public decimal CookingTemperature { get; set; }
        [Required]
        [Range(0.01, 100)]
        public decimal SaltPercentage { get; set; }
        [Required]
        [Range(0, 120)]
        public int AgingMonths { get; set; }
        [Required]
        [Range(0, 29)]
        public int AgingDays { get; set; }
    }
}