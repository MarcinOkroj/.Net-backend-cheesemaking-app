using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class FavouritesRepository : IFavouritesRepository
    {
        private readonly ApplicationDBContext _context;
        public FavouritesRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Favourites> CreateAsync(Favourites favourites)
        {
            await _context.Favourites.AddAsync(favourites);
            await _context.SaveChangesAsync();
            return favourites;
        }

        public async Task<Favourites> DeleteFavourites(AppUser appUser, int id)
        {
            var favouritesModel = await _context.Favourites.FirstOrDefaultAsync(x => x.AppUserId == appUser.Id && x.Recipe.Id == id);

            if (favouritesModel == null)
            {
                return null;
            }

            _context.Favourites.Remove(favouritesModel);
            await _context.SaveChangesAsync();
            return favouritesModel;
        }

        public async Task<List<Recipe>> GetUserFavourites(AppUser user)
        {
            return await _context.Favourites.Where(u => u.AppUserId == user.Id)
            .Select(recipe => new Recipe
            {
                Id = recipe.RecipeId,
                Name = recipe.Recipe.Name,
                Style = recipe.Recipe.Style,
                Complexity = recipe.Recipe.Complexity,
                Bacterias = recipe.Recipe.Bacterias,
                ExpectedYield = recipe.Recipe.ExpectedYield,
                MilkVolume = recipe.Recipe.MilkVolume,
                CookingTemperature = recipe.Recipe.CookingTemperature,
                SaltPercentage = recipe.Recipe.SaltPercentage,
                AgingMonths = recipe.Recipe.AgingMonths,
                AgingDays = recipe.Recipe.AgingDays,
            }).ToListAsync();
        }
    }
}