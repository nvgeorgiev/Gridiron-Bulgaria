namespace GridironBulgaria.Test.Controllers.Tests
{
    using GridironBulgaria.Web.Controllers;
    using GridironBulgaria.Web.Models;
    using GridironBulgaria.Web.ViewModels.PhotoAlbums;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class PhotoAlbumsControllerTest
    {
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
        public void CreatePostShouldReturnRedirectAndShouldSaveTeamWithValidTeam(
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
    }
}
