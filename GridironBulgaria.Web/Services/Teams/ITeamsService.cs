namespace GridironBulgaria.Web.Services.Teams
{
    using GridironBulgaria.Web.Models;
    using GridironBulgaria.Web.ViewModels.Teams;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITeamsService
    {
        Task<IEnumerable<TeamInfoViewModel>> GetAllTeamsAsync();

        Task<string> CreateAsync(CreateTeamInputModel inputModel);

        Task<TeamDetailsViewModel> TeamDetailsAsync(string name);

        Task DeleteByIdAsync(int id);

        // HttpGet Edit Method
        Task<EditTeamViewModel> EditTeamViewAsync(int id);

        // HttpPost Edit Method
        Task<string> EditTeamAsync(EditTeamViewModel editInputModel);

        Task<Team> GetTeamByIdAsync(int id);
    }
}
