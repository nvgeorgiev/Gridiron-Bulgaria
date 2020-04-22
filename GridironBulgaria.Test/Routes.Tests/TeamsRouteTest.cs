namespace GridironBulgaria.Test.Routes.Tests
{
    using GridironBulgaria.Web.Controllers;
    using GridironBulgaria.Web.ViewModels.Teams;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class TeamsRouteTest
    {
        [Fact]
        public void CreateGetShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Teams/Create")
                .To<TeamsController>(c => c.Create());

        [Fact]
        public void DetailsActionShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/teams/details/test-name")
                .To<TeamsController>(c => c.Details("test-name"));

    }
}
