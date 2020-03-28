namespace GridironBulgaria.Web.Services.Games
{
    using GridironBulgaria.Web.ViewModels.Games;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IGamesService
    {
        Task<IEnumerable<GameViewModel>> GetAllGamesAsync();
    }
}
