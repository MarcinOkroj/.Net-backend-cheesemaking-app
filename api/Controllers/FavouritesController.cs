using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace api.Controllers
{
    [Route("api/favourites")]
    [ApiController]
    public class FavouritesController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IRecipeRepository _recipeRepo;
        private readonly IFavouritesRepository _favouritesRepo;
        public FavouritesController(UserManager<AppUser> userManager,
        IRecipeRepository recipeRepo, IFavouritesRepository favouritesRepo)
        {
            _userManager = userManager;
            _recipeRepo = recipeRepo;
            _favouritesRepo = favouritesRepo;
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation(
        Summary = "Retrieves a user's favorite recipes",
        Description = "Fetches detailed information about the recipes that the user has saved to their favorites",
        OperationId = "GetUserFavourites"
        )]
        public async Task<IActionResult> GetUserFavourites()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userFavourites = await _favouritesRepo.GetUserFavourites(appUser);
            var userFavouritesDto = userFavourites.Select(recipe => recipe.ToRecipeDto()).ToList();
            return Ok(userFavouritesDto);
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation(
        Summary = "Adds a recipe to the user's favorites",
        Description = "Allows the user to add a specific recipe to their favorites list by providing the recipe id.",
        OperationId = "AddRecipeToFavorites"
        )]
        public async Task<IActionResult> AddRecipeToFavourites(int id)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var recipe = await _recipeRepo.GetByIdAsync(id);

            if (recipe == null) return BadRequest("Recipe not found");

            var userFavourites = await _favouritesRepo.GetUserFavourites(appUser);

            if (userFavourites.Any(element => element.Id == id)) return BadRequest("Cannot add same recipe to favourites");

            var favouritesModel = new Favourites
            {
                RecipeId = recipe.Id,
                AppUserId = appUser.Id
            };

            await _favouritesRepo.CreateAsync(favouritesModel);

            if (favouritesModel == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        }

        [HttpDelete]
        [Authorize]
        [SwaggerOperation(
    Summary = "Removes a recipe from the user's favorites",
    Description = "Allows the user to remove a specific recipe from their favorites list by providing the recipe id.",
    OperationId = "DeleteRecipeFromFavorites"
        )]
        public async Task<IActionResult> DeleteRecipeFromFavourites(int id)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var userFavourites = await _favouritesRepo.GetUserFavourites(appUser);

            var filteredRecipe = userFavourites.Where(recipe => recipe.Id == id).ToList();

            if (filteredRecipe.Count() == 1)
            {
                await _favouritesRepo.DeleteFavourites(appUser, id);
            }
            else
            {
                return BadRequest("Recipe not in your favourites");
            }

            return Ok();
        }

    }
}