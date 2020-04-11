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

        Task<EditGameViewModel> EditGameViewAsync(int id);

        Task<int> EditGameAsync(EditGameViewModel editInputModel);

        Task<Game> GetGameByIdAsync(int id);
    }
}