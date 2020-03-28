namespace GridironBulgaria.Web.Services.Games
{
    using GridironBulgaria.Web.Data;
    using GridironBulgaria.Web.ViewModels.Games;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class GamesService : IGamesService
    {
        private readonly ApplicationDbContext database;

        public GamesService(ApplicationDbContext database)
        {
            this.database = database;
        }

        public async Task<IEnumerable<GameViewModel>> GetAllGamesAsync()
        {
            var allGames = await this.database.Games.Select(x => new GameViewModel
            {
                Id = x.Id,
                DateAndStartTime = x.DateAndStartTime,
                StadiumLocationUrl = x.StadiumLocationUrl,
                Format = x.Format,
                HomeTeamScore = x.HomeTeamScore,
                AwayTeamScore = x.AwayTeamScore,
                HomeTeamName = x.HomeTeam.Name ?? x.HomeForeignTeamName,
                HomeTeamLogoUrl = x.HomeTeam.LogoUrl ?? x.HomeForeignTeamLogoUrl,
                AwayTeamName = x.HomeTeam.Name ?? x.AwayForeignTeamName,
                AwayTeamLogoUrl = x.AwayTeam.LogoUrl ?? x.AwayForeignTeamLogoUrl,
            }).ToListAsync();

            return allGames;
        }
    }
}
