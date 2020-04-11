namespace GridironBulgaria.Web.Services.Teams
{
    using GridironBulgaria.Web.Data;
    using GridironBulgaria.Web.Models;
    using GridironBulgaria.Web.ViewModels.Teams;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TeamsService : ITeamsService
    {
        private readonly ApplicationDbContext database;

        public TeamsService(ApplicationDbContext database)
        {
            this.database = database;
        }

        public async Task<IEnumerable<TeamInfoViewModel>> GetAllTeamsAsync()
        {
            var allTeams = await this.database.Teams.Select(x => new TeamInfoViewModel
            {
                Id = x.Id,
                Name = x.Name,
                LogoUrl = x.LogoUrl,
                CountryName = x.Town.Country.Name,
            }).ToListAsync();

            return allTeams;
        }

        public async Task<int> CreateAsync(CreateTeamInputModel inputModel)
        {
            var country = await this.database.Countries.FirstOrDefaultAsync(c => c.Name.ToLower() == inputModel.CountryName.ToLower());

            if (country == null)
            {
                country = new Country
                {
                    Name = inputModel.CountryName,
                };

                await this.database.Countries.AddAsync(country);
            }

            var town = await this.database.Towns.FirstOrDefaultAsync(t => t.Name.ToLower() == inputModel.TownName.ToLower());

            if (town == null)
            {
                town = new Town
                {
                    Name = inputModel.TownName,
                    Country = country,
                };

                await this.database.Towns.AddAsync(town);
            }

            var team = await this.database.Teams.FirstOrDefaultAsync(tm => tm.Name.ToLower() == inputModel.Name.ToLower());

            if (team == null)
            {
                team = new Team
                {
                    Name = inputModel.Name,
                    LogoUrl = inputModel.LogoUrl,
                    CoverPhotoUrl = inputModel.CoverPhotoUrl,
                    CoachName = inputModel.CoachName,
                    TrainingsDescription = inputModel.TrainingsDescription,
                    ContactUrl = inputModel.ContactUrl,
                    Town = town,
                };

                await this.database.Teams.AddAsync(team);
                await this.database.SaveChangesAsync();
            }

            return team.Id;
        }

        public async Task<TeamDetailsViewModel> TeamDetailsAsync(int id)
        {
            var team = await this.GetTeamByIdAsync(id);

            var teamPhotoAlbums = await this.database.PhotoAlbums.Where(pa => pa.HomeTeamId == team.Id || pa.AwayTeamId == team.Id).ToListAsync();

            var gamesPlayed = await this.database.Games.Where(g => (g.HomeTeamId == team.Id || g.AwayTeamId == team.Id) && g.DateAndStartTime.Contains("КРАЙ")).ToListAsync();

            var teamDetails = new TeamDetailsViewModel
            {
                Id = team.Id,
                LogoUrl = team.LogoUrl,
                Name = team.Name,
                CoverPhotoUrl = team.CoverPhotoUrl,
                CoachName = team.CoachName,
                TrainingsDescription = team.TrainingsDescription,
                ContactUrl = team.ContactUrl,
                GamesPlayedCounter = gamesPlayed.Count,
                TeamPhotoAlbums = teamPhotoAlbums,
            };

            return teamDetails;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var team = await this.GetTeamByIdAsync(id);

            this.database.Teams.Remove(team);
            await this.database.SaveChangesAsync();
        }

        // HttpGet Edit Method
        public async Task<EditTeamViewModel> EditTeamViewAsync(int id)
        {
            var teamToEdit = await this.GetTeamByIdAsync(id);

            var townName = await this.database.Towns.FirstOrDefaultAsync(t => t.Id == teamToEdit.TownId);

            var countryName = await this.database.Countries.FirstOrDefaultAsync(c => c.Id == townName.CountryId);

            var editTeamInput = new EditTeamViewModel
            {
                Id = teamToEdit.Id,
                Name = teamToEdit.Name,
                LogoUrl = teamToEdit.LogoUrl,
                CoverPhotoUrl = teamToEdit.CoverPhotoUrl,
                CoachName = teamToEdit.CoachName,
                TrainingsDescription = teamToEdit.TrainingsDescription,
                ContactUrl = teamToEdit.ContactUrl,
                CountryName = countryName.Name,
                TownName = townName.Name,
            };

            return editTeamInput;
        }

        // HttpPost Edit Method
        public async Task<int> EditTeamAsync(EditTeamViewModel editInputModel)
        {
            var country = await this.database.Countries.FirstOrDefaultAsync(c => c.Name.ToLower() == editInputModel.CountryName.ToLower());

            if (country == null)
            {
                country = new Country
                {
                    Name = editInputModel.CountryName,
                };

                await this.database.Countries.AddAsync(country);
            }

            var town = await this.database.Towns.FirstOrDefaultAsync(t => t.Name.ToLower() == editInputModel.TownName.ToLower());

            if (town == null)
            {
                town = new Town
                {
                    Name = editInputModel.TownName,
                    Country = country,
                };

                await this.database.Towns.AddAsync(town);
            }

            var team = new Team
            {
                Id = editInputModel.Id,
                Name = editInputModel.Name,
                LogoUrl = editInputModel.LogoUrl,
                CoverPhotoUrl = editInputModel.CoverPhotoUrl,
                CoachName = editInputModel.CoachName,
                TrainingsDescription = editInputModel.TrainingsDescription,
                ContactUrl = editInputModel.ContactUrl,
                Town = town,
            };

            this.database.Teams.Update(team);
            await this.database.SaveChangesAsync();

            return team.Id;
        }

        public async Task<Team> GetTeamByIdAsync(int id)
            => await this.database.Teams.FirstOrDefaultAsync(x => x.Id == id);
    }
}
