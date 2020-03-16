﻿namespace GridironBulgaria.Web.Services.Teams
{
    using GridironBulgaria.Web.ViewModels.Teams;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ITeamsService
    {
        void Create(CreateTeamInputModel inputModel);

        IEnumerable<TeamInfoViewModel> GetAll();
    }
}
