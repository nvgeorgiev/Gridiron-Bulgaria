﻿namespace GridironBulgaria.Test.Controllers.Tests
{
    using GridironBulgaria.Test.TestData;
    using GridironBulgaria.Web.Controllers;
    using GridironBulgaria.Web.Models;
    using GridironBulgaria.Web.ViewModels.Teams;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class TeamsControllerTest
    {
        [Fact]
        public void IndexShouldReturnAllTeams()
            => MyController<TeamsController>
                .Instance(instance => instance
                    .WithData(TeamTestData.GetTeams(5)))
                .Calling(c => c.Index())
                .ShouldReturn()
                .View(view => view
                     .WithModelOfType<IEnumerable<TeamInfoViewModel>>());

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

        [Fact]
        public void CreatePostShouldReturnViewWithTheSameModelWhenModelStateIsInvalid()
            => MyController<TeamsController>
                .Instance()
                .Calling(c => c.Create(With.Default<CreateTeamInputModel>()))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(result => result
                    .WithModelOfType<CreateTeamInputModel>()
                    .Passing(team => team.Name == null));

        [Theory]
        [InlineData("Test Name", "Test Country", "Test Town")]
        public void CreatePostShouldReturnRedirectAndShouldSaveTeamWithValidTeam(string name, string country, string town)
            => MyController<TeamsController>
                .Instance()
                .WithUser(user => user.InRole("Admin"))
                .Calling(c => c.Create(new CreateTeamInputModel
                {
                    Name = name,
                    CountryName = country,
                    TownName = town,
                }))
                .ShouldHave()
                .ValidModelState()
                .AndAlso()
                .ShouldHave()
                .Data(data => data
                    .WithSet<Team>(set =>
                    {
                        set.SingleOrDefault(team => team.Name == name);
                    }))
                .AndAlso()
                .ShouldReturn()
                .Redirect(result => result
                    .To<TeamsController>(c => c.Details(name.ToLower().Replace(' ', '-'))));

    }
}
