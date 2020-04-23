namespace GridironBulgaria.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using GridironBulgaria.Web.Services.Games;
    using GridironBulgaria.Web.ViewModels.Games;

    public class GamesController : Controller
    {
        private readonly IGamesService gamesService;

        public GamesController(IGamesService gamesService)
        {
            this.gamesService = gamesService;
        }

        public async Task<IActionResult> Index(string search)
        {
            var allGames = await this.gamesService.GetAllGamesAsync(search);

            return this.View(allGames);
        }

        [HttpGet]
        [Authorize(Policy = "RequireAdminRole")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
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
            if (!this.ModelState.IsValid)
            {
                return this.NotFound();
            }

            await this.gamesService.DeleteByIdAsync(id);

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
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

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Edit(EditGameViewModel editInput)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(editInput);
            }

            await this.gamesService.EditGameAsync(editInput);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}