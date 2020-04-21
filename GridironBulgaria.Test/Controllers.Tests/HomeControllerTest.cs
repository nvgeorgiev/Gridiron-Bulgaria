namespace GridironBulgaria.Test.Controllers.Tests
{
    using MyTested.AspNetCore.Mvc;
    using GridironBulgaria.Web.Controllers;
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
