using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IFavouritesRepository
    {
        Task<List<Recipe>> GetUserFavourites(AppUser user);
        Task<Favourites> CreateAsync(Favourites favourites);
        Task<Favourites> DeleteFavourites(AppUser appUser, int id);
    }
}