namespace GridironBulgaria.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using GridironBulgaria.Web.Services.Games;
    using Microsoft.AspNetCore.Mvc;

    public class GamesController : Controller
    {
        private readonly IGamesService gamesService;

        public GamesController(IGamesService gamesService)
        {
            this.gamesService = gamesService;
        }

        public async Task<IActionResult> Index()
        {
            var allGames = await this.gamesService.GetAllGamesAsync();

            return this.View(allGames);
        }
    }
}