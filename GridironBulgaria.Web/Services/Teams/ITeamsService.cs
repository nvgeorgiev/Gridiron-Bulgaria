namespace GridironBulgaria.Web.Services.Teams
{
    using GridironBulgaria.Web.Models;
    using GridironBulgaria.Web.ViewModels.Teams;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ITeamsService
    {
        Task<int> CreateAsync(CreateTeamInputModel inputModel);

        IEnumerable<TeamInfoViewModel> GetAllTeams();

        //Task<TeamDetailsViewModel> TeamDetails(int id);

        Task<Team> GetTeamByIdAsync(int id);

        Task DeleteByIdAsync(int id);
    }
}
