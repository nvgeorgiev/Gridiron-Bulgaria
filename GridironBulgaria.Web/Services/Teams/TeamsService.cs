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
            var country = this.database.Countries.Where(c => c.Name.ToLower() == inputModel.CountryName.ToLower()).FirstOrDefault();

            if (country == null)
            {
                country = new Country
                {
                    Name = inputModel.CountryName,
                };

                this.database.Countries.Add(country);
            }

            var town = this.database.Towns.Where(t => t.Name.ToLower() == inputModel.TownName.ToLower()).FirstOrDefault();

            if (town == null)
            {
                town = new Town
                {
                    Name = inputModel.TownName,
                    Country = country,
                };

                this.database.Towns.Add(town);
            }

            var team = this.database.Teams.Where(tm => tm.Name.ToLower() == inputModel.Name.ToLower()).FirstOrDefault();

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

                this.database.Teams.Add(team);
                this.database.SaveChanges();
            }                      
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