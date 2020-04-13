namespace GridironBulgaria.Web.Controllers
{
    using GridironBulgaria.Web.Services.Games;
    using GridironBulgaria.Web.ViewModels.Games;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class GamesController : Controller
    {
        private readonly IGamesService gamesService;

        public GamesController(IGamesService gamesService)
        {
            this.gamesService = gamesService;
        }

        public async Task<IActionResult> Index(string id)
        {
            var allGames = await this.gamesService.GetAllGamesAsync(id);

            return this.View(allGames);
        }

        [Authorize(Policy = "RequireAdminRole")]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateGameViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.gamesService.GameCreateAsync(input);

            return this.RedirectToAction(nameof(this.Index));
        }

        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Delete(int id)
        {
            await this.gamesService.DeleteByIdAsync(id);

            return this.RedirectToAction(nameof(this.Index));
        }

        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Edit(int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.NotFound();
            }

            var editViewModel = await this.gamesService.EditGameViewAsync(id);

            return this.View(editViewModel);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditGameViewModel editInput)
        {
            if (!this.ModelState.IsValid)
            {
                return this.NotFound();
            }

            await this.gamesService.EditGameAsync(editInput);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}