namespace GridironBulgaria.Web.Controllers
{
    using GridironBulgaria.Web.Models;
    using GridironBulgaria.Web.Services.PhotoAlbums;
    using GridironBulgaria.Web.ViewModels.PhotoAlbums;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class PhotoAlbumsController : Controller
    {
        private readonly IPhotoAlbumsService photoAlbumsService;

        public PhotoAlbumsController(IPhotoAlbumsService photoAlbumsService)
        {
            this.photoAlbumsService = photoAlbumsService;
        }

        [Route("gallery")]
        public async Task<IActionResult> Index(string search)
        {
            var allTeams = await this.photoAlbumsService.GetAllPhotoAlbumsAsync(search);

            return this.View(allTeams);
        }

        [HttpGet]
        [Authorize(Policy = "RequireAdminRole")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Create(CreatePhotoAlbumViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.photoAlbumsService.PhotoAlbumCreateAsync(input);

            return this.RedirectToAction(nameof(this.Index));
        }

        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Delete(int id)
        {
            await this.photoAlbumsService.DeleteByIdAsync(id);

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

            var editViewModel = await this.photoAlbumsService.EditPhotoAlbumViewAsync(id);

            return this.View(editViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Edit(EditPhotoAlbumViewModel editInput)
        {
            if (!this.ModelState.IsValid)
            {
                return this.NotFound();
            }

            await this.photoAlbumsService.EditPhotoAlbumAsync(editInput);

            return this.RedirectToAction(nameof(this.Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}