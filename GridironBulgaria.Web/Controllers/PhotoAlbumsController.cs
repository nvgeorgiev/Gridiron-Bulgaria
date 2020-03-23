namespace GridironBulgaria.Web.Controllers
{
    using GridironBulgaria.Web.Services.PhotoAlbums;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using GridironBulgaria.Web.Data;
    using GridironBulgaria.Web.Models;
    using GridironBulgaria.Web.ViewModels.Teams;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;    

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
    }
}