namespace GridironBulgaria.Test.Routing.Tests
{
    using GridironBulgaria.Web.Controllers;
    using GridironBulgaria.Web.ViewModels.Teams;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class TeamsRouteTest
    {
        [Fact]
        public void IndexAllTeamsShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Teams/Index")
                .To<TeamsController>(c => c.Index());

        [Fact]
        public void CreateGetShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Teams/Create")
                .To<TeamsController>(c => c.Create());

        [Theory]
        [InlineData("Test Name", "TestCountryName", "TestTownName")]
        public void CreatePostShouldBeRoutedCorrectlyAndHaveValidModelState(string name, string coutryName, string townName)
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Post)
                    .WithLocation("Teams/Create")
                    .WithFormFields(new
                    {
                        Name = name,
                        CountryName = coutryName,
                        TownName = townName,
                    }))
                .To<TeamsController>(c => c.Create(new CreateTeamInputModel
                {
                    Name = name,
                    CountryName = coutryName,
                    TownName = townName,
                }))
                .AndAlso()
                .ToValidModelState();

        [Fact]
        public void DetailsShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/teams/details/test-name")
                .To<TeamsController>(c => c.Details("test-name"));

        [Fact]
        public void DeleteShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Teams/Delete/1")
                .To<TeamsController>(c => c.Delete(1));

        [Fact]
        public void EditGetShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Teams/Edit/1")
                .To<TeamsController>(c => c.Edit(1));

        [Theory]
        [InlineData("Test Name", "TestCountryName", "TestTownName")]
        public void EditPostShouldBeRoutedCorrectlyAndHaveValidModelState(string name, string coutryName, string townName)
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Post)
                    .WithLocation("Teams/Edit/1")
                    .WithFormFields(new
                    {                        
                        Name = name,
                        CountryName = coutryName,
                        TownName = townName,
                    }))
                .To<TeamsController>(c => c.Edit(new EditTeamViewModel
                {
                    Id = 1,
                    Name = name,
                    CountryName = coutryName,
                    TownName = townName,
                }))
                .AndAlso()
                .ToValidModelState();
    }
}
