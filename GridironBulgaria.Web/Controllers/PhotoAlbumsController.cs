namespace GridironBulgaria.Web.Controllers
{
    using GridironBulgaria.Web.Models;
    using GridironBulgaria.Web.Services.PhotoAlbums;
    using GridironBulgaria.Web.ViewModels.PhotoAlbums;
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

        public async Task<IActionResult> Index()
        {
            var allTeams = await this.photoAlbumsService.GetAllPhotoAlbumsAsync();

            return this.View(allTeams);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePhotoAlbumsViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var photoAlbumsId = await this.photoAlbumsService.PhotoAlbumsCreateAsync(input);

            return this.Redirect("/PhotoAlbums");

            //return this.RedirectToAction(nameof(this.Details), new { id = teamId });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}