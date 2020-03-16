namespace GridironBulgaria.Web.Services.Teams
{
    using GridironBulgaria.Web.Data;
    using GridironBulgaria.Web.Models;
    using GridironBulgaria.Web.ViewModels.Teams;
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

        public void Create(CreateTeamInputModel inputModel)
        {
            var country = this.database.Countries.Where(c => c.Name == inputModel.CountryName).FirstOrDefault();

            if (country == null)
            {
                country = new Country
                {
                    Name = inputModel.CountryName,
                };

                this.database.Countries.Add(country);
            }

            var town = this.database.Towns.Where(t => t.Name == inputModel.TownName).FirstOrDefault();

            if (town == null)
            {
                town = new Town
                {
                    Name = inputModel.TownName,
                    Country = country,
                };

                this.database.Towns.Add(town);
            }


            var team = new Team
            {
                Name = inputModel.Name,
                LogoUrl = inputModel.LogoUrl,
                CoverPhotoUrl = inputModel.CoverPhotoUrl,
                CoachName = inputModel.CoachName,
                TrainingsDescription = inputModel.TrainingsDescription,
                ContactUrl = inputModel.ContactUrl,
                Town = town,
            };

            this.database.Teams.Add(team);
            this.database.SaveChanges();
        }

        public IEnumerable<TeamInfoViewModel> GetAll()
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
    }
}