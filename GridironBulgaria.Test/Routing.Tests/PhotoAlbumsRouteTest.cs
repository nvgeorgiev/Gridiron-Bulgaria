namespace GridironBulgaria.Test.Routing.Tests
{
    using GridironBulgaria.Web.Controllers;
    using GridironBulgaria.Web.ViewModels.PhotoAlbums;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class PhotoAlbumsRouteTest
    {
        [Fact]
        public void IndexAllAlbumsShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/gallery")
                .To<PhotoAlbumsController>(c => c.Index(null));

        [Theory]
        [InlineData("test")]
        public void IndexAllGamesSearchShouldBeRoutedCorrectly(string search)
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                .WithLocation("/gallery?=search=test")
                .WithFormFields(new
                {
                    Search = search,
                }))
                .To<PhotoAlbumsController>(c => c.Index(search));

        [Fact]
        public void CreateGetShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/PhotoAlbums/Create")
                .To<PhotoAlbumsController>(c => c.Create());

        [Theory]
        [InlineData("TestTitle", "TestThumbnailPhotoUrl", "TestFacebookAlbumUrl", "TestEventDate")]
        public void CreatePostShouldBeRoutedCorrectlyAndHaveValidModelState(
            string title, string thumbnailPhotoUrl,
            string facebookAlbumUrl, string eventDate)
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Post)
                    .WithLocation("/PhotoAlbums/Create")
                    .WithFormFields(new
                    {
                        Title = title,
                        ThumbnailPhotoUrl = thumbnailPhotoUrl,
                        FacebookAlbumUrl = facebookAlbumUrl,
                        EventDate = eventDate,
                    }))
                .To<PhotoAlbumsController>(c => c.Create(new CreatePhotoAlbumViewModel
                {
                    Title = title,
                    ThumbnailPhotoUrl = thumbnailPhotoUrl,
                    FacebookAlbumUrl = facebookAlbumUrl,
                    EventDate = eventDate,
                }))
                .AndAlso()
                .ToValidModelState();

        [Fact]
        public void DeleteShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/PhotoAlbums/Delete/1")
                .To<PhotoAlbumsController>(c => c.Delete(1));

        [Fact]
        public void EditGetShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/PhotoAlbums/Edit/1")
                .To<PhotoAlbumsController>(c => c.Edit(1));

        [Theory]
        [InlineData("TestTitle", "TestThumbnailPhotoUrl", "TestFacebookAlbumUrl", "TestEventDate")]
        public void EditPostShouldBeRoutedCorrectlyAndHaveValidModelState(
            string title, string thumbnailPhotoUrl,
            string facebookAlbumUrl, string eventDate)
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Post)
                    .WithLocation("PhotoAlbums/Edit/1")
                    .WithFormFields(new
                    {
                        Title = title,
                        ThumbnailPhotoUrl = thumbnailPhotoUrl,
                        FacebookAlbumUrl = facebookAlbumUrl,
                        EventDate = eventDate,
                    }))
                .To<PhotoAlbumsController>(c => c.Edit(new EditPhotoAlbumViewModel
                {
                    Id = 1,
                    Title = title,
                    ThumbnailPhotoUrl = thumbnailPhotoUrl,
                    FacebookAlbumUrl = facebookAlbumUrl,
                    EventDate = eventDate,
                }))
                .AndAlso()
                .ToValidModelState();
    }
}
