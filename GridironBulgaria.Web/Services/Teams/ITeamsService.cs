namespace GridironBulgaria.Web.Services.Teams
{
    using GridironBulgaria.Web.Models;
    using GridironBulgaria.Web.ViewModels.Teams;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITeamsService
    {
        Task<IEnumerable<TeamInfoViewModel>> GetAllTeamsAsync();

        Task<int> CreateAsync(CreateTeamInputModel inputModel);

        Task<TeamDetailsViewModel> TeamDetailsAsync(int id);

        Task DeleteByIdAsync(int id);

        Task<EditTeamViewModel> EditTeamViewAsync(int id);

        Task<int> EditTeamAsync(EditTeamViewModel editInputModel);

        Task<Team> GetTeamByIdAsync(int id);
    }
}
