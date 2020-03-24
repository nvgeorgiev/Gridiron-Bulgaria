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

        public async Task<IEnumerable<PhotoAlbumViewModel>> GetAllPhotoAlbumsAsync()
        {
            var allPhotoAlbums = await this.database.PhotoAlbums.Select(x => new PhotoAlbumViewModel
            {
                Id = x.Id,
                Title = x.Title,
                ThumbnailPhotoUrl = x.ThumbnailPhotoUrl,
                FacebookAlbumUrl = x.FacebookAlbumUrl,
                EventDate = x.EventDate,
            }).ToListAsync();

            return allPhotoAlbums;
        }

        public async Task<int> PhotoAlbumCreateAsync(CreatePhotoAlbumViewModel inputModel)
        {
            Team homeTeam = null;

            Team awayTeam = null;

            if (inputModel.HomeTeamName != null)
            {
                homeTeam = await this.database.Teams.FirstOrDefaultAsync(h => h.Name.ToLower() == inputModel.HomeTeamName.ToLower());
            }

            if (inputModel.AwayTeamName != null)
            {
                awayTeam = await this.database.Teams.FirstOrDefaultAsync(a => a.Name.ToLower() == inputModel.AwayTeamName.ToLower());
            }

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

        public async Task DeleteByIdAsync(int id)
        {
            var photoAlbum = await GetPhotoAlbumByIdAsync(id);

            this.database.PhotoAlbums.Remove(photoAlbum);
            await this.database.SaveChangesAsync();
        }

        // HttpGet Edit Method
        public async Task<EditPhotoAlbumViewModel> EditPhotoAlbumViewAsync(int id)
        {
            var photoAlbumToEdit = await GetPhotoAlbumByIdAsync(id);

            Team homeTeam = null;

            Team awayTeam = null;

            if (photoAlbumToEdit.HomeTeamId != null)
            {
                homeTeam = await this.database.Teams.FirstOrDefaultAsync(h => h.Id == photoAlbumToEdit.HomeTeamId);
            }

            if (photoAlbumToEdit.AwayTeamId != null)
            {
                awayTeam = await this.database.Teams.FirstOrDefaultAsync(a => a.Id == photoAlbumToEdit.AwayTeamId);
            }

            EditPhotoAlbumViewModel editPhotoAlbumInput;

            if (homeTeam != null && awayTeam == null)
            {
                editPhotoAlbumInput = new EditPhotoAlbumViewModel
                {
                    Id = photoAlbumToEdit.Id,
                    Title = photoAlbumToEdit.Title,
                    ThumbnailPhotoUrl = photoAlbumToEdit.ThumbnailPhotoUrl,
                    FacebookAlbumUrl = photoAlbumToEdit.FacebookAlbumUrl,
                    EventDate = photoAlbumToEdit.EventDate,
                    HomeTeamName = homeTeam.Name,
                    AwayTeamName = null,
                };
            }
            else if (homeTeam == null && awayTeam != null)
            {
                editPhotoAlbumInput = new EditPhotoAlbumViewModel
                {
                    Id = photoAlbumToEdit.Id,
                    Title = photoAlbumToEdit.Title,
                    ThumbnailPhotoUrl = photoAlbumToEdit.ThumbnailPhotoUrl,
                    FacebookAlbumUrl = photoAlbumToEdit.FacebookAlbumUrl,
                    EventDate = photoAlbumToEdit.EventDate,
                    HomeTeamName = null,
                    AwayTeamName = awayTeam.Name,
                };
            }
            else if (homeTeam == null && awayTeam == null)
            {
                editPhotoAlbumInput = new EditPhotoAlbumViewModel
                {
                    Id = photoAlbumToEdit.Id,
                    Title = photoAlbumToEdit.Title,
                    ThumbnailPhotoUrl = photoAlbumToEdit.ThumbnailPhotoUrl,
                    FacebookAlbumUrl = photoAlbumToEdit.FacebookAlbumUrl,
                    EventDate = photoAlbumToEdit.EventDate,
                    HomeTeamName = null,
                    AwayTeamName = null,
                };
            }
            else
            {
                editPhotoAlbumInput = new EditPhotoAlbumViewModel
                {
                    Id = photoAlbumToEdit.Id,
                    Title = photoAlbumToEdit.Title,
                    ThumbnailPhotoUrl = photoAlbumToEdit.ThumbnailPhotoUrl,
                    FacebookAlbumUrl = photoAlbumToEdit.FacebookAlbumUrl,
                    EventDate = photoAlbumToEdit.EventDate,
                    HomeTeamName = homeTeam.Name,
                    AwayTeamName = awayTeam.Name,
                };
            }
            return editPhotoAlbumInput;
        }

        // HttpPost Edit Method
        public async Task<int> EditPhotoAlbumAsync(EditPhotoAlbumViewModel editInputModel)
        {
            Team homeTeam = null;

            Team awayTeam = null;

            if (editInputModel.HomeTeamName != null)
            {
                homeTeam = await this.database.Teams.FirstOrDefaultAsync(h => h.Name.ToLower() == editInputModel.HomeTeamName.ToLower());
            }

            if (editInputModel.AwayTeamName != null)
            {
                awayTeam = await this.database.Teams.FirstOrDefaultAsync(a => a.Name.ToLower() == editInputModel.AwayTeamName.ToLower());
            }

            var photoAlbum = new PhotoAlbum
            {
                Id = editInputModel.Id,
                Title = editInputModel.Title,
                ThumbnailPhotoUrl = editInputModel.ThumbnailPhotoUrl,
                FacebookAlbumUrl = editInputModel.FacebookAlbumUrl,
                EventDate = editInputModel.EventDate,
                HomeTeam = homeTeam,
                AwayTeam = awayTeam,
            };

            this.database.PhotoAlbums.Update(photoAlbum);
            await this.database.SaveChangesAsync();

            return photoAlbum.Id;
        }

        public async Task<PhotoAlbum> GetPhotoAlbumByIdAsync(int id)
            => await this.database.PhotoAlbums.FirstOrDefaultAsync(x => x.Id == id);
    }
}
