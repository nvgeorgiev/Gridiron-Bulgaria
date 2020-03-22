﻿namespace GridironBulgaria.Web.Services.Teams
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

            var gamesPlayed = await this.database.Games.Where(gp => gp.HomeTeamId == team.Id || gp.AwayTeamId == team.Id).ToListAsync();

            var teamPhotoAlbums = await this.database.PhotoAlbums.Where(gp => gp.HomeTeamId == team.Id || gp.AwayTeamId == team.Id).ToListAsync();

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

        public async Task<Team> GetTeamByIdAsync(int id)
            => await this.database.Teams.FirstOrDefaultAsync(x => x.Id == id);
    }
}