﻿namespace GridironBulgaria.Web.Services.Games
{
    using GridironBulgaria.Web.Data;
    using GridironBulgaria.Web.Models;
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
                AwayTeamName = x.AwayTeam.Name ?? x.AwayForeignTeamName,
                AwayTeamLogoUrl = x.AwayTeam.LogoUrl ?? x.AwayForeignTeamLogoUrl,
            }).ToListAsync();

            return allGames;
        }

        public async Task<int> GameCreateAsync(CreateGameViewModel inputModel)
        {
            var homeTeam = await this.database.Teams.FirstOrDefaultAsync(h => h.Name.ToLower() == inputModel.HomeTeamName.ToLower());

            var awayTeam = await this.database.Teams.FirstOrDefaultAsync(a => a.Name.ToLower() == inputModel.AwayTeamName.ToLower()); ;

            var homeForeignTeamName = "";
            var homeForeignTeamLogoUrl = "";
            var awayForeignTeamName = "";
            var awayForeignTeamLogoUrl = "";

            if (homeTeam == null)
            {
                homeForeignTeamName = inputModel.HomeTeamName;
                homeForeignTeamLogoUrl = inputModel.HomeTeamLogoUrl;
            }

            if (awayTeam == null)
            {
                awayForeignTeamName = inputModel.AwayTeamName;
                awayForeignTeamLogoUrl = inputModel.AwayTeamLogoUrl;
            }

            var game = new Game();

            if (homeTeam == null && awayTeam != null)
            {
                game = new Game
                {
                    DateAndStartTime = inputModel.DateAndStartTime,
                    StadiumLocationUrl = inputModel.StadiumLocationUrl,
                    Format = inputModel.Format,
                    HomeTeamScore = inputModel.HomeTeamScore,
                    AwayTeamScore = inputModel.AwayTeamScore,
                    HomeForeignTeamName = homeForeignTeamName,
                    HomeForeignTeamLogoUrl = homeForeignTeamLogoUrl,
                    AwayTeam = awayTeam,
                };
            }
            else if (homeTeam != null && awayTeam == null)
            {
                game = new Game
                {
                    DateAndStartTime = inputModel.DateAndStartTime,
                    StadiumLocationUrl = inputModel.StadiumLocationUrl,
                    Format = inputModel.Format,
                    HomeTeamScore = inputModel.HomeTeamScore,
                    AwayTeamScore = inputModel.AwayTeamScore,
                    HomeTeam = homeTeam,
                    AwayForeignTeamName = awayForeignTeamName,
                    AwayForeignTeamLogoUrl = awayForeignTeamLogoUrl,
                };
            }
            else if (homeTeam == null && awayTeam == null)
            {
                game = new Game
                {
                    DateAndStartTime = inputModel.DateAndStartTime,
                    StadiumLocationUrl = inputModel.StadiumLocationUrl,
                    Format = inputModel.Format,
                    HomeTeamScore = inputModel.HomeTeamScore,
                    AwayTeamScore = inputModel.AwayTeamScore,
                    HomeForeignTeamName = homeForeignTeamName,
                    HomeForeignTeamLogoUrl = homeForeignTeamLogoUrl,
                    AwayForeignTeamName = awayForeignTeamName,
                    AwayForeignTeamLogoUrl = awayForeignTeamLogoUrl,
                };
            }
            else
            {
                game = new Game
                {
                    DateAndStartTime = inputModel.DateAndStartTime,
                    StadiumLocationUrl = inputModel.StadiumLocationUrl,
                    Format = inputModel.Format,
                    HomeTeamScore = inputModel.HomeTeamScore,
                    AwayTeamScore = inputModel.AwayTeamScore,
                    HomeTeam = homeTeam,
                    AwayTeam = awayTeam,
                };
            }

            await this.database.Games.AddAsync(game);
            await this.database.SaveChangesAsync();

            return game.Id;
        }
    }
}
