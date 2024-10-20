using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Enums;

namespace api.Dtos.Recipe
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Style { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ComplexityLevel Complexity { get; set; }
        public string Bacterias { get; set; } = string.Empty;
        public decimal ExpectedYield { get; set; }
        public decimal MilkVolume { get; set; }
        public decimal CookingTemperature { get; set; }
        public decimal SaltPercentage { get; set; }
        public int AgingMonths { get; set; }
        public int AgingDays { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}