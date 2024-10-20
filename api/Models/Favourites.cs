using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Favourites")]
    public class Favourites
    {
        public string AppUserId { get; set; }
        public int RecipeId { get; set; }
        public AppUser AppUser { get; set; }
        public Recipe Recipe { get; set; }
    }
}