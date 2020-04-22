namespace GridironBulgaria.Test.Routing.Tests
{
    using GridironBulgaria.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class HomeRouteTest
    {
        [Fact]
        public void IndexGetShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Index")
                .To<HomeController>(c => c.Index());
    }
}
