using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api.Enums;

namespace api.Models
{
    [Table("Recipes")]
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Style { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ComplexityLevel Complexity { get; set; }
        public string Bacterias { get; set; } = string.Empty;
        [Column(TypeName = "decimal(9,2)")]
        public decimal ExpectedYield { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal MilkVolume { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal CookingTemperature { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal SaltPercentage { get; set; }
        public int AgingMonths { get; set; }
        public int AgingDays { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Favourites> Favourites { get; set; } = new List<Favourites>();
    }
}