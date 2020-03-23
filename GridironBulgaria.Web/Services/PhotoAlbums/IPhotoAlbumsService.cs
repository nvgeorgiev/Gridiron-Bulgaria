namespace GridironBulgaria.Web.Services.PhotoAlbums
{
    using GridironBulgaria.Web.ViewModels.PhotoAlbums;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPhotoAlbumsService
    {
        Task<IEnumerable<PhotoAlbumsViewModel>> GetAllPhotoAlbumsAsync();

        Task<int> PhotoAlbumsCreateAsync(CreatePhotoAlbumsViewModel inputModel);
    }
}
