namespace GridironBulgaria.Web.Services.PhotoAlbums
{
    using GridironBulgaria.Web.Data;
    using GridironBulgaria.Web.Models;
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

        public async Task<int> PhotoAlbumsCreateAsync(CreatePhotoAlbumsViewModel inputModel)
        {
            var homeTeam = await this.database.Teams.FirstOrDefaultAsync(h => h.Name.ToLower() == inputModel.HomeTeamName.ToLower());

            var awayTeam = await this.database.Teams.FirstOrDefaultAsync(a => a.Name.ToLower() == inputModel.AwayTeamName.ToLower());

            var photoAlbum = new PhotoAlbum
            {
                Title = inputModel.Title,
                ThumbnailPhotoUrl = inputModel.ThumbnailPhotoUrl,
                FacebookAlbumUrl = inputModel.FacebookAlbumUrl,
                EventDate = inputModel.EventDate,
                HomeTeam = homeTeam,
                AwayTeam = awayTeam,
            };

            await this.database.PhotoAlbums.AddAsync(photoAlbum);
            await this.database.SaveChangesAsync();

            return photoAlbum.Id;
        }
    }
}
