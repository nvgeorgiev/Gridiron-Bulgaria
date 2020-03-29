﻿namespace GridironBulgaria.Web.Services.Games
{
    using GridironBulgaria.Web.Models;
    using GridironBulgaria.Web.ViewModels.Games;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IGamesService
    {
        Task<IEnumerable<GameViewModel>> GetAllGamesAsync();

        Task<int> GameCreateAsync(CreateGameViewModel inputModel);

        Task DeleteByIdAsync(int id);

        Task<Game> GetGameByIdAsync(int id);

    }
}