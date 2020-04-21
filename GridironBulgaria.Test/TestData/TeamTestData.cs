namespace GridironBulgaria.Test.TestData
{
    using GridironBulgaria.Web.Models;
    using System.Collections.Generic;
    using System.Linq;
    using MyTested.AspNetCore.Mvc;
    using System;

    public static class TeamTestData
    {
        public static List<Team> GetTeams(int count)
        {
            var team = Enumerable
                 .Range(1, count)
                 .Select(i => new Team
                 {
                     Id = i,
                     Name = $"TestName {i}",
                     LogoUrl = $"TestLogoUrl {i}",
                     CoverPhotoUrl = $"TestCoverPhotoUrl {i}",
                     CoachName = $"TestCoachName {i}",
                     TrainingsDescription = $"TestTrainingsDescription {i}",
                     ContactUrl = $"TestContactUrl {i}",
                     TownId = i,
                     Town = new Town
                     {
                         Id = i,
                         Name = $"TestTown {i}",
                         CountryId = i,
                         Country = new Country
                         {
                             Id = i,
                             Name = $"TestCountry {i}"
                         }
                     },
                     HomeGames = new HashSet<Game>(),
                     AwayGames = new HashSet<Game>(),
                     HomePhotoAlbums = new HashSet<PhotoAlbum>(),
                     AwayPhotoAlbums = new HashSet<PhotoAlbum>(),
                 })
                 .ToList();

            return team;
        }
    }
}
