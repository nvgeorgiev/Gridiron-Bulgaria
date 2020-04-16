namespace GridironBulgaria.Web.Services.Games
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

        public async Task<IEnumerable<GameViewModel>> GetAllGamesAsync(string id)
        {
            var allGames = this.database.Games.Select(x => new GameViewModel
            {
                Id = x.Id,
                DateAndStartTime = x.DateAndStartTime,
                StadiumLocationUrl = x.StadiumLocationUrl,
                Format = x.Format,
                HomeTeamScore = x.HomeTeamScore,
                AwayTeamScore = x.AwayTeamScore,
                HomeTeamName = x.HomeTeam.Name ?? x.HomeForeignTeamName,
                HomeTeamLogoUrl = x.HomeTeam.LogoUrl ?? x.HomeForeignTeamLogoUrl,
                HomeTeamCountry = x.HomeTeam.Town.Country.Name,
                AwayTeamName = x.AwayTeam.Name ?? x.AwayForeignTeamName,
                AwayTeamLogoUrl = x.AwayTeam.LogoUrl ?? x.AwayForeignTeamLogoUrl,
                AwayTeamCountry = x.AwayTeam.Town.Country.Name,
            });

            if (!String.IsNullOrEmpty(id))
            {
                allGames = allGames.Where(x =>
                x.HomeTeamName.Contains(id) ||
                x.AwayTeamName.Contains(id) ||
                x.DateAndStartTime.Contains(id) ||
                x.Format.Contains(id));
            }

            return await allGames.ToListAsync();
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

        public async Task DeleteByIdAsync(int id)
        {
            var game = await GetGameByIdAsync(id);

            this.database.Games.Remove(game);
            await this.database.SaveChangesAsync();
        }

        // HttpGet Edit Method
        public async Task<EditGameViewModel> EditGameViewAsync(int id)
        {
            var gameToEdit = await GetGameByIdAsync(id);

            var editGameInput = new EditGameViewModel
            {
                Id = gameToEdit.Id,
                DateAndStartTime = gameToEdit.DateAndStartTime,
                StadiumLocationUrl = gameToEdit.StadiumLocationUrl,
                HomeTeamScore = gameToEdit.HomeTeamScore,
                AwayTeamScore = gameToEdit.AwayTeamScore,
            };

            return editGameInput;
        }

        // HttpPost Edit Method
        public async Task<int> EditGameAsync(EditGameViewModel editInputModel)
        {
            var game = await GetGameByIdAsync(editInputModel.Id);

            game.DateAndStartTime = editInputModel.DateAndStartTime;
            game.StadiumLocationUrl = editInputModel.StadiumLocationUrl;
            game.HomeTeamScore = editInputModel.HomeTeamScore;
            game.AwayTeamScore = editInputModel.AwayTeamScore;

            this.database.Games.Update(game);
            await this.database.SaveChangesAsync();

            return game.Id;
        }

        public async Task<Game> GetGameByIdAsync(int id)
           => await this.database.Games.FirstOrDefaultAsync(x => x.Id == id);
    }
}
