namespace GridironBulgaria.Test.Controllers.Tests
{
    using GridironBulgaria.Web.Controllers;
    using GridironBulgaria.Web.Models;
    using GridironBulgaria.Web.ViewModels.Games;
    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class GamesControllerTest
    {
        [Fact]
        public void IndexShouldReturnAllGames()
            => MyController<GamesController>
                .Instance(instance => instance
                    .WithData(new Game
                    {
                        Id = 1,
                        DateAndStartTime = "TestDateAndStartTime 1",
                        StadiumLocationUrl = "TestStadiumLocationUrl 1",
                        Format = "TestFormat 1",                       
                    }))
                .Calling(c => c.Index(null))
                .ShouldReturn()
                .View(view => view
                     .WithModelOfType<IEnumerable<GameViewModel>>());

        [Theory]
        [InlineData("DateAndStartTimeTest")]
        public void IndexShouldReturnAllAlbumsBySearchCriteria(string search)
            => MyController<GamesController>
                .Instance(instance => instance
                    .WithData(new Game
                    {
                        Id = 1,
                        DateAndStartTime = "TestDateAndStartTime 1",
                        StadiumLocationUrl = "TestStadiumLocationUrl 1",
                        Format = "TestFormat 1",
                    }))
                .Calling(c => c.Index(search))
                .ShouldReturn()
                .View(view => view
                     .WithModelOfType<IEnumerable<GameViewModel>>()
                     .Passing(gameModel => gameModel.Where(x => x.DateAndStartTime.Contains(search))));

        [Fact]
        public void CreateGetShouldHaveRestrictionsForHttpGetOnlyAndAuthorizedUserAdminAndShouldReturnView()
            => MyController<GamesController>
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
            => MyController<GamesController>
                .Instance()
                .Calling(c => c.Create(With.Default<CreateGameViewModel>()))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());

        [Fact]
        public void CreatePostShouldReturnViewWithTheSameModelWhenModelStateIsInvalid()
            => MyController<GamesController>
                .Instance()
                .Calling(c => c.Create(With.Default<CreateGameViewModel>()))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(result => result
                    .WithModelOfType<CreateGameViewModel>()
                    .Passing(game => game.DateAndStartTime == null));

        [Theory]
        [InlineData(1, "TestDateAndStartTime", "TestStadiumLocationUrl", "TestFormat", 0, 0, "TestHomeTeamName", "TestAwayTeamName")]
        public void CreatePostShouldReturnRedirectAndShouldSaveGameWithValidGame(
            int id, string dateAndStartTime, string stadiumLocationUrl, string format, int homeTeamScore, 
            int awayTeamScore, string homeTeamName, string awayTeamName)
            => MyController<GamesController>
                .Instance()
                .WithUser(user => user.InRole("Admin"))
                .Calling(c => c.Create(new CreateGameViewModel
                {
                    Id = id,
                    DateAndStartTime = dateAndStartTime,
                    StadiumLocationUrl = stadiumLocationUrl,
                    Format = format,
                    HomeTeamScore = homeTeamScore,
                    AwayTeamScore = awayTeamScore,
                    HomeTeamName = homeTeamName,
                    AwayTeamName = awayTeamName,
                }))
                .ShouldHave()
                .ValidModelState()
                .AndAlso()
                .ShouldHave()
                .Data(data => data
                    .WithSet<Game>(set =>
                    {
                        set.SingleOrDefault(game => game.Id == id);
                    }))
                .AndAlso()
                .ShouldReturn()
                .Redirect(result => result
                    .To<GamesController>(c => c.Index(null)));

        [Fact]
        public void DeleteShouldDeleteGameAndRedirectWhenValidId()
           => MyController<GamesController>
               .Instance(instance => instance
                   .WithUser(user => user.InRole("Admin"))
                   .WithData(new Game
                   {
                       Id = 1,
                       DateAndStartTime = "TestDateAndStartTime 1",
                       StadiumLocationUrl = "TestStadiumLocationUrl 1",
                       Format = "TestFormat 1",
                   }))
               .Calling(c => c.Delete(1))
               .ShouldHave()
               .Data(data => data
                   .WithSet<Game>(set => set.ShouldBeEmpty()))
               .AndAlso()
               .ShouldReturn()
               .Redirect(redirect => redirect
                   .To<GamesController>(c => c.Index(null)));

        [Fact]
        public void EditGetShouldReturnViewWithCorrectModelWhenUserAdmin()
            => MyController<GamesController>
                .Instance(instance => instance
                    .WithUser(user => user.InRole("Admin"))
                    .WithData(new Game
                    {
                        Id = 1,
                        DateAndStartTime = "TestDateAndStartTime 1",
                        StadiumLocationUrl = "TestStadiumLocationUrl 1",
                        Format = "TestFormat 1",
                    }))
                .Calling(c => c.Edit(1))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<EditGameViewModel>()
                    .Passing(game => game.DateAndStartTime == "TestDateAndStartTime 1"));

        [Fact]
        public void EditPostShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<GamesController>
                .Calling(c => c.Edit(With.Default<EditGameViewModel>()))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());

        [Fact]
        public void EditPostShouldReturnViewWithSameModelWhenInvalidModelState()
            => MyController<GamesController>
                .Instance(instance => instance
                    .WithUser()
                    .WithData(new Game
                    {
                        Id = 1,
                        DateAndStartTime = "TestDateAndStartTime 1",
                        StadiumLocationUrl = "TestStadiumLocationUrl 1",
                        Format = "TestFormat 1",
                    }))
                .Calling(c => c.Edit(With.Default<EditGameViewModel>()))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(With.Default<EditGameViewModel>());
    }
}
