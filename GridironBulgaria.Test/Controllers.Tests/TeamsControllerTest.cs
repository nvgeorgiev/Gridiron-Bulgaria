namespace GridironBulgaria.Test.Controllers.Tests
{
    using GridironBulgaria.Web.Controllers;
    using GridironBulgaria.Web.ViewModels.Teams;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class TeamsControllerTest
    {
        [Fact]
        public void CreateGetShouldHaveRestrictionsForHttpGetOnlyAndAuthorizedUserAdminAndShouldReturnView()
            => MyController<TeamsController>
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
            => MyController<TeamsController>
                .Instance()
                .Calling(c => c.Create(With.Default<CreateTeamInputModel>()))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());

    }
}
