namespace GridironBulgaria.Web.Services.Teams
{
    using GridironBulgaria.Web.Data;
    using GridironBulgaria.Web.Models;
    using GridironBulgaria.Web.ViewModels.Teams;
    using Microsoft.EntityFrameworkCore;
    using System;
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

        public IEnumerable<TeamInfoViewModel> GetAllTeams()
        {
            var allTeams = this.database.Teams.Select(x => new TeamInfoViewModel
            {
                Id = x.Id,
                Name = x.Name,
                LogoUrl = x.LogoUrl,
                CountryName = x.Town.Country.Name,
            }).ToList();

            return allTeams;
        }

        public void DeleteById(int id)
        {
            var team = this.GetTeamById(id);

            this.database.Teams.Remove(team);
            this.database.SaveChanges();
        }

        public Team GetTeamById(int id)
            => this.database.Teams.FirstOrDefault(x => x.Id == id);
    }
}