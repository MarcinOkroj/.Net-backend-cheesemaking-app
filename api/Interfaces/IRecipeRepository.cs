using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Recipe;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IRecipeRepository
    {
        Task<List<Recipe>> GetAllAsync(QueryObject query);
        Task<Recipe?> GetByIdAsync(int id);
        Task<Recipe> CreateAsync(Recipe recipeModel);
        Task<Recipe?> UpdateAsync(int id, UpdateRecipeRequestDto recipeDto);
        Task<Recipe?> DeleteAsync(int id);
        Task<bool> RecipeExists(int id);
    }
}