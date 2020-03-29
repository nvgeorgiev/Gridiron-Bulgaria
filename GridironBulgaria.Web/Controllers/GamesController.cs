namespace GridironBulgaria.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using GridironBulgaria.Web.Services.Games;
    using GridironBulgaria.Web.ViewModels.Games;
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

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGameViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.gamesService.GameCreateAsync(input);

            return this.Redirect("/Games");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.gamesService.DeleteByIdAsync(id);

            return this.Redirect("/Games");
        }
    }
}