﻿namespace GridironBulgaria.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using GridironBulgaria.Web.Models;
    using GridironBulgaria.Web.Services.Teams;
    using GridironBulgaria.Web.ViewModels.Teams;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class TeamsController : Controller
    {
        private readonly ITeamsService teamsService;

        public TeamsController(ITeamsService teamsService)
        {
            this.teamsService = teamsService;
        }

        public async Task<IActionResult> Index()
        {
            var allTeams = await this.teamsService.GetAllTeamsAsync();

            return this.View(allTeams);
        }

        [Authorize(Policy = "RequireAdminRole")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateTeamInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var teamId = await this.teamsService.CreateAsync(input);

            return this.RedirectToAction(nameof(this.Details), new { id = teamId });
        }

        public async Task<IActionResult> Details(int id)
        {
            if (!this.ModelState.IsValid)
            {
                return NotFound();
            }

            var teamDetails = await this.teamsService.TeamDetailsAsync(id);

            return this.View(teamDetails);
        }

        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Delete(int id)
        {
            await this.teamsService.DeleteByIdAsync(id);

            return this.Redirect("/Teams");
        }

        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Edit(int id)
        {
            if (!this.ModelState.IsValid)
            {
                return NotFound();
            }

            var editViewModel = await this.teamsService.EditTeamViewAsync(id);

            return this.View(editViewModel);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditTeamViewModel editInput)
        {
            if (!this.ModelState.IsValid)
            {
                return NotFound();
            }

            var teamId = await this.teamsService.EditTeamAsync(editInput);

            return this.RedirectToAction(nameof(this.Details), new { id = teamId });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}