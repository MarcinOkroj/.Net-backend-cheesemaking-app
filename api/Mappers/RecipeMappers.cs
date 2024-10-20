using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Recipe;
using api.Models;

namespace api.Mappers
{
    public static class RecipeMappers
    {
        public static RecipeDto ToRecipeDto(this Recipe recipeModel)
        {
            return new RecipeDto
            {
                Id = recipeModel.Id,
                Comments = recipeModel.Comments.Select(c => c.ToCommentDto()).ToList(),
                Name = recipeModel.Name,
                Style = recipeModel.Style,
                Description = recipeModel.Description,
                Complexity = recipeModel.Complexity,
                Bacterias = recipeModel.Bacterias,
                ExpectedYield = recipeModel.ExpectedYield,
                MilkVolume = recipeModel.MilkVolume,
                CookingTemperature = recipeModel.CookingTemperature,
                SaltPercentage = recipeModel.SaltPercentage,
                AgingMonths = recipeModel.AgingMonths,
                AgingDays = recipeModel.AgingDays
            };
        }

        public static Recipe ToRecipeFromCreateDTO(this CreateRecipeRequestDto recipeDto)
        {
            return new Recipe
            {
                Name = recipeDto.Name,
                Style = recipeDto.Style,
                Description = recipeDto.Description,
                Complexity = recipeDto.Complexity,
                Bacterias = recipeDto.Bacterias,
                ExpectedYield = recipeDto.ExpectedYield,
                MilkVolume = recipeDto.MilkVolume,
                CookingTemperature = recipeDto.CookingTemperature,
                SaltPercentage = recipeDto.SaltPercentage,
                AgingMonths = recipeDto.AgingMonths,
                AgingDays = recipeDto.AgingDays
            };
        }
    }
}