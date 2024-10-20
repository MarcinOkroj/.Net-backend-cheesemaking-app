using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Recipe;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace api.Controllers
{
    [Route("api/recipe")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IRecipeRepository _recipeRepo;
        public RecipeController(ApplicationDBContext context, IRecipeRepository recipeRepo)
        {
            _recipeRepo = recipeRepo;
            _context = context;
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation(
        Summary = "Retrieves specific recipes by their parameters",
        Description = "Gets detailed information about recipes, including its name, style, bacteria cultures, compexity etc.",
        OperationId = "GetRecipes"
        )]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var recipes = await _recipeRepo.GetAllAsync(query);

            var recipesDto = recipes.Select(recipe => recipe.ToRecipeDto()).ToList();

            return Ok(recipesDto);
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(
        Summary = "Retrieves specific recipe by id",
        Description = "Gets detailed information about the recipe, including its name, style, bacteria cultures, compexity etc.",
        OperationId = "GetRecipeById"
        )]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var recipe = await _recipeRepo.GetByIdAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(recipe.ToRecipeDto());
        }

        [HttpPost]
        [SwaggerOperation(
        Summary = "Creates a recipe",
        Description = "Creates a new recipe by providing specific parameters in the request body",
        OperationId = "CreateRecipe"
        )]
        public async Task<IActionResult> Create([FromBody] CreateRecipeRequestDto recipeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var recipeModel = recipeDto.ToRecipeFromCreateDTO();

            await _recipeRepo.CreateAsync(recipeModel);

            return CreatedAtAction(nameof(GetById), new { id = recipeModel.Id }, recipeModel.ToRecipeDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        [SwaggerOperation(
        Summary = "Updates the recipe",
        Description = "Updates the recipe by providing id and specific parameters in the request body",
        OperationId = "UpdateRecipe"
        )]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRecipeRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var recipeModel = await _recipeRepo.UpdateAsync(id, updateDto);

            if (recipeModel == null)
            {
                return NotFound();
            }

            return Ok(recipeModel.ToRecipeDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        [SwaggerOperation(
        Summary = "Deletes the recipe",
        Description = "Deletes the recipe by providing id",
        OperationId = "UpdateRecipe"
        )]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var recipeModel = await _recipeRepo.DeleteAsync(id);

            if (recipeModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}