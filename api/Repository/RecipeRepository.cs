using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Recipe;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly ApplicationDBContext _context;
        public RecipeRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Recipe> CreateAsync(Recipe recipeModel)
        {
            await _context.Recipes.AddAsync(recipeModel);
            await _context.SaveChangesAsync();
            return recipeModel;
        }

        public async Task<Recipe?> DeleteAsync(int id)
        {
            var recipeModel = await _context.Recipes.FirstOrDefaultAsync(x => x.Id == id);

            if (recipeModel == null)
            {
                return null;
            }

            _context.Recipes.Remove(recipeModel);
            await _context.SaveChangesAsync();
            return recipeModel;
        }

        public async Task<List<Recipe>> GetAllAsync(QueryObject query)
        {
            var recipes = _context.Recipes.Include(c => c.Comments).ThenInclude(a => a.AppUser).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                recipes = recipes.Where(recipe => recipe.Name.Contains(query.Name));
            }

            if (!string.IsNullOrWhiteSpace(query.Style))
            {
                recipes = recipes.Where(recipe => recipe.Style.Contains(query.Style));
            }

            if (!string.IsNullOrWhiteSpace(query.Bacterias))
            {
                recipes = recipes.Where(recipe => recipe.Bacterias.Contains(query.Bacterias));
            }

            if (!string.IsNullOrWhiteSpace(query.Complexity))
            {
                recipes = recipes.Where(recipe => recipe.Complexity.ToString().Contains(query.Complexity));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    recipes = query.IsDecsending ? recipes.OrderByDescending(recipe => recipe.Name) : recipes.OrderBy(recipe => recipe.Name);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;


            return await recipes.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Recipe?> GetByIdAsync(int id)
        {
            return await _context.Recipes.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }

        public Task<bool> RecipeExists(int id)
        {
            return _context.Recipes.AnyAsync(recipe => recipe.Id == id);
        }

        public async Task<Recipe?> UpdateAsync(int id, UpdateRecipeRequestDto recipeDto)
        {
            var existingRecipe = await _context.Recipes.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRecipe == null)
            {
                return null;
            }

            existingRecipe.Name = recipeDto.Name;
            existingRecipe.Style = recipeDto.Style;
            existingRecipe.Description = recipeDto.Description;
            existingRecipe.Complexity = recipeDto.Complexity;
            existingRecipe.Bacterias = recipeDto.Bacterias;
            existingRecipe.ExpectedYield = recipeDto.ExpectedYield;
            existingRecipe.MilkVolume = recipeDto.MilkVolume;
            existingRecipe.CookingTemperature = recipeDto.CookingTemperature;
            existingRecipe.SaltPercentage = recipeDto.SaltPercentage;
            existingRecipe.AgingMonths = recipeDto.AgingMonths;
            existingRecipe.AgingDays = recipeDto.AgingDays;

            await _context.SaveChangesAsync();

            return existingRecipe;
        }
    }
}