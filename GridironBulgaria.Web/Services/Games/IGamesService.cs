namespace GridironBulgaria.Web.Services.Games
{
    using GridironBulgaria.Web.Models;
    using GridironBulgaria.Web.ViewModels.Games;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGamesService
    {
        Task<IEnumerable<GameViewModel>> GetAllGamesAsync(string id);

        Task<int> GameCreateAsync(CreateGameViewModel inputModel);

        Task DeleteByIdAsync(int id);

        // HttpGet Edit Method
        Task<EditGameViewModel> EditGameViewAsync(int id);

        // HttpPost Edit Method
        Task<int> EditGameAsync(EditGameViewModel editInputModel);

        Task<Game> GetGameByIdAsync(int id);
    }
}