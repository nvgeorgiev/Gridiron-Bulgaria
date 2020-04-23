namespace GridironBulgaria.Test.Routing.Tests
{
    using GridironBulgaria.Web.Controllers;
    using GridironBulgaria.Web.ViewModels.Games;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class GamesRouteTest
    {
        [Fact]
        public void IndexAllGamesShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Games/Index")
                .To<GamesController>(c => c.Index(null));

        [Theory]
        [InlineData("test")]
        public void IndexAllGamesSearchShouldBeRoutedCorrectly(string search)
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                .WithLocation("/Games?=search=test")
                .WithFormFields(new
                {
                    Search = search,
                }))
                .To<GamesController>(c => c.Index(search));

        [Fact]
        public void CreateGetShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Games/Create")
                .To<GamesController>(c => c.Create());

        [Theory]
        [InlineData("TestDateAndStartTime", "TestStadiumLocationUrl", "TestFormat", 0, 0,
            "TestHomeTeamName", "TestAwayTeamName")]
        public void CreatePostShouldBeRoutedCorrectlyAndHaveValidModelState(
            string dateAndStartTime, string stadiumLocationUrl,
            string format, int homeTeamScore, int awayTeamScore,
            string homeTeamName, string awayTeamName)
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Post)
                    .WithLocation("/Games/Create")
                    .WithFormFields(new
                    {
                        DateAndStartTime = dateAndStartTime,
                        StadiumLocationUrl = stadiumLocationUrl,
                        Format = format,
                        HomeTeamScore = homeTeamScore,
                        AwayTeamScore = awayTeamScore,
                        HomeTeamName = homeTeamName,
                        AwayTeamName = awayTeamName,
                    }))
                .To<GamesController>(c => c.Create(new CreateGameViewModel
                {
                    DateAndStartTime = dateAndStartTime,
                    StadiumLocationUrl = stadiumLocationUrl,
                    Format = format,
                    HomeTeamScore = homeTeamScore,
                    AwayTeamScore = awayTeamScore,
                    HomeTeamName = homeTeamName,
                    AwayTeamName = awayTeamName,
                }))
                .AndAlso()
                .ToValidModelState();

        [Fact]
        public void DeleteShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Games/Delete/1")
                .To<GamesController>(c => c.Delete(1));

        [Fact]
        public void EditGetShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Games/Edit/1")
                .To<GamesController>(c => c.Edit(1));

        [Theory]
        [InlineData("TestDateTime", "TestStadiumUrl", 0, 0)]
        public void EditPostShouldBeRoutedCorrectlyAndHaveValidModelState(
            string dateTime, string stadiumUrl,
            int homeTeamScore, int awayTeamScore)
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Post)
                    .WithLocation("Games/Edit/1")
                    .WithFormFields(new
                    {
                        DateAndStartTime = dateTime,
                        StadiumLocationUrl = stadiumUrl,                        
                        HomeTeamScore = homeTeamScore,
                        AwayTeamScore = awayTeamScore,                        
                    }))
                .To<GamesController>(c => c.Edit(new EditGameViewModel
                {
                    Id = 1,
                    DateAndStartTime = dateTime,
                    StadiumLocationUrl = stadiumUrl,
                    HomeTeamScore = homeTeamScore,
                    AwayTeamScore = awayTeamScore,
                }))
                .AndAlso()
                .ToValidModelState();
    }
}
