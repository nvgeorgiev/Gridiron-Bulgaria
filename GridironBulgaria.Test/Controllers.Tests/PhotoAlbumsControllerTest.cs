namespace GridironBulgaria.Test.Controllers.Tests
{
    using GridironBulgaria.Web.Controllers;
    using GridironBulgaria.Web.Models;
    using GridironBulgaria.Web.ViewModels.PhotoAlbums;
    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class PhotoAlbumsControllerTest
    {
        [Fact]
        public void IndexShouldReturnAllAlbums()
            => MyController<PhotoAlbumsController>
                .Instance(instance => instance
                    .WithData(new PhotoAlbum
                    {
                        Id = 1,
                        Title = "TestTitle 1",
                        ThumbnailPhotoUrl = "TestThumbnailPhotoUrl 1",
                        FacebookAlbumUrl = "TestFacebookAlbumUrl 1",
                        EventDate = "TestEventDate 1",
                    }))
                .Calling(c => c.Index(null))
                .ShouldReturn()
                .View(view => view
                     .WithModelOfType<IEnumerable<PhotoAlbumViewModel>>());

        [Theory]
        [InlineData("TitleTest")]
        public void IndexShouldReturnAllAlbumsBySearchCriteria(string search)
            => MyController<PhotoAlbumsController>
                .Instance(instance => instance
                    .WithData(new PhotoAlbum
                    {
                        Id = 1,
                        Title = "TestTitleTest",
                        ThumbnailPhotoUrl = "TestThumbnailPhotoUrl 1",
                        FacebookAlbumUrl = "TestFacebookAlbumUrl 1",
                        EventDate = "TestEventDate 1",
                    }))
                .Calling(c => c.Index(search))
                .ShouldReturn()
                .View(view => view
                     .WithModelOfType<IEnumerable<PhotoAlbumViewModel>>()
                     .Passing(photoAlbumModel => photoAlbumModel.Where(x => x.Title.Contains(search))));

        [Fact]
        public void CreateGetShouldHaveRestrictionsForHttpGetOnlyAndAuthorizedUserAdminAndShouldReturnView()
            => MyController<PhotoAlbumsController>
                .Instance()
                .Calling(c => c.Create())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Get)
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void CreatePostShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUserAdmin()
            => MyController<PhotoAlbumsController>
                .Instance()
                .Calling(c => c.Create(With.Default<CreatePhotoAlbumViewModel>()))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());

        [Fact]
        public void CreatePostShouldReturnViewWithTheSameModelWhenModelStateIsInvalid()
            => MyController<PhotoAlbumsController>
                .Instance()
                .Calling(c => c.Create(With.Default<CreatePhotoAlbumViewModel>()))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(result => result
                    .WithModelOfType<CreatePhotoAlbumViewModel>()
                    .Passing(album => album.Title == null));

        [Theory]
        [InlineData(1, "TestTitle", "TestThumbnailPhotoUrl", "TestFacebookAlbumUrl", "TestEventDate")]
        public void CreatePostShouldReturnRedirectAndShouldSaveAlbumWithValidAlbum(
            int id, string title, string thumbnailPhotoUrl, string facebookAlbumUrl, string eventDate)
            => MyController<PhotoAlbumsController>
                .Instance()
                .WithUser(user => user.InRole("Admin"))
                .Calling(c => c.Create(new CreatePhotoAlbumViewModel
                {
                    Id = id,
                    Title = title,
                    ThumbnailPhotoUrl = thumbnailPhotoUrl,
                    FacebookAlbumUrl = facebookAlbumUrl,
                    EventDate = eventDate,
                }))
                .ShouldHave()
                .ValidModelState()
                .AndAlso()
                .ShouldHave()
                .Data(data => data
                    .WithSet<PhotoAlbum>(set =>
                    {
                        set.SingleOrDefault(album => album.Id == id);
                    }))
                .AndAlso()
                .ShouldReturn()
                .Redirect(result => result
                    .To<PhotoAlbumsController>(c => c.Index(null)));

        [Fact]
        public void DeleteShouldDeleteAlbumAndRedirectWhenValidId()
           => MyController<PhotoAlbumsController>
               .Instance(instance => instance
                   .WithUser(user => user.InRole("Admin"))
                   .WithData(new PhotoAlbum
                   {
                       Id = 1,
                       Title = "TestTitle 1",
                       ThumbnailPhotoUrl = "TestThumbnailPhotoUrl 1",
                       FacebookAlbumUrl = "TestFacebookAlbumUrl 1",
                       EventDate = "TestEventDate 1",                       
                   }))
               .Calling(c => c.Delete(1))
               .ShouldHave()
               .Data(data => data
                   .WithSet<PhotoAlbum>(set => set.ShouldBeEmpty()))
               .AndAlso()
               .ShouldReturn()
               .Redirect(redirect => redirect
                   .To<PhotoAlbumsController>(c => c.Index(null)));

        [Fact]
        public void EditGetShouldReturnViewWithCorrectModelWhenUserAdmin()
            => MyController<PhotoAlbumsController>
                .Instance(instance => instance
                    .WithUser(user => user.InRole("Admin"))
                    .WithData(new PhotoAlbum
                    {
                        Id = 1,
                        Title = "TestTitle 1",
                        ThumbnailPhotoUrl = "TestThumbnailPhotoUrl 1",
                        FacebookAlbumUrl = "TestFacebookAlbumUrl 1",
                        EventDate = "TestEventDate 1",
                    }))
                .Calling(c => c.Edit(1))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<EditPhotoAlbumViewModel>()
                    .Passing(album => album.Title == "TestTitle 1"));

        [Fact]
        public void EditPostShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<PhotoAlbumsController>
                .Calling(c => c.Edit(With.Default<EditPhotoAlbumViewModel>()))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());

        [Fact]
        public void EditPostShouldReturnViewWithSameModelWhenInvalidModelState()
            => MyController<PhotoAlbumsController>
                .Instance(instance => instance
                    .WithUser()
                    .WithData(new PhotoAlbum
                    {
                        Id = 1,
                        Title = "TestTitle 1",
                        ThumbnailPhotoUrl = "TestThumbnailPhotoUrl 1",
                        FacebookAlbumUrl = "TestFacebookAlbumUrl 1",
                        EventDate = "TestEventDate 1",
                    }))
                .Calling(c => c.Edit(With.Default<EditPhotoAlbumViewModel>()))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(With.Default<EditPhotoAlbumViewModel>());
    }
}
