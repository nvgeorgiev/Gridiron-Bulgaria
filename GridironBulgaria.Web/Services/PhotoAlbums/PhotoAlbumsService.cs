namespace GridironBulgaria.Web.Services.PhotoAlbums
{
    using GridironBulgaria.Web.Data;
    using GridironBulgaria.Web.ViewModels.PhotoAlbums;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PhotoAlbumsService : IPhotoAlbumsService
    {
        private readonly ApplicationDbContext database;

        public PhotoAlbumsService(ApplicationDbContext database)
        {
            this.database = database;
        }

        public async Task<IEnumerable<PhotoAlbumsViewModel>> GetAllPhotoAlbumsAsync()
        {
            var allPhotoAlbums = await this.database.PhotoAlbums.Select(x => new PhotoAlbumsViewModel
            {
                Id = x.Id,
                Title = x.Title,
                ThumbnailPhotoUrl = x.ThumbnailPhotoUrl,
                FacebookAlbumUrl = x.FacebookAlbumUrl,
                EventDate = x.EventDate,
            }).ToListAsync();

            return allPhotoAlbums;
        }
    }
}