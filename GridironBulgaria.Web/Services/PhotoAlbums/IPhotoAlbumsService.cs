namespace GridironBulgaria.Web.Services.PhotoAlbums
{
    using GridironBulgaria.Web.Models;
    using GridironBulgaria.Web.ViewModels.PhotoAlbums;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPhotoAlbumsService
    {
        Task<IEnumerable<PhotoAlbumViewModel>> GetAllPhotoAlbumsAsync(string id);

        Task<int> PhotoAlbumCreateAsync(CreatePhotoAlbumViewModel inputModel);

        Task DeleteByIdAsync(int id);

        // HttpGet Edit Method
        Task<EditPhotoAlbumViewModel> EditPhotoAlbumViewAsync(int id);

        // HttpPost Edit Method
        Task<int> EditPhotoAlbumAsync(EditPhotoAlbumViewModel editInputModel);

        Task<PhotoAlbum> GetPhotoAlbumByIdAsync(int id);
    }
}
