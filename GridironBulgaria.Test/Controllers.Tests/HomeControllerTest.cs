namespace GridironBulgaria.Test.Controllers.Tests
{
    using GridironBulgaria.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Index())
                .ShouldReturn()
                .View();
    }
}
