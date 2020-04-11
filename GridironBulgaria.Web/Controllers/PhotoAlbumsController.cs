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

        public async Task<IActionResult> Index(string id)
        {
            var allTeams = await this.photoAlbumsService.GetAllPhotoAlbumsAsync(id);

            return this.View(allTeams);
        }

        [Authorize(Policy = "RequireAdminRole")]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePhotoAlbumViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.photoAlbumsService.PhotoAlbumCreateAsync(input);

            return this.Redirect("/PhotoAlbums");            
        }

        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Delete(int id)
        {
            await this.photoAlbumsService.DeleteByIdAsync(id);

            return this.Redirect("/PhotoAlbums");
        }

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

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditPhotoAlbumViewModel editInput)
        {
            if (!this.ModelState.IsValid)
            {
                return this.NotFound();
            }

            await this.photoAlbumsService.EditPhotoAlbumAsync(editInput);

            return this.Redirect("/PhotoAlbums");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}