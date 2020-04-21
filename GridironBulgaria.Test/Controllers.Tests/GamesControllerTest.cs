namespace GridironBulgaria.Test.Controllers.Tests
{
    using GridironBulgaria.Web.Controllers;
    using GridironBulgaria.Web.Models;
    using GridironBulgaria.Web.ViewModels.Games;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class GamesControllerTest
    {
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
        public void CreatePostShouldReturnRedirectAndShouldSaveTeamWithValidTeam(
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
    }
}
