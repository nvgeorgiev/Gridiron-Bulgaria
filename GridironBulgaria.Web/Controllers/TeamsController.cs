namespace GridironBulgaria.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using GridironBulgaria.Web.Models;
    using GridironBulgaria.Web.Services.Teams;
    using GridironBulgaria.Web.ViewModels.Teams;
    using Microsoft.AspNetCore.Mvc;

    public class TeamsController : Controller
    {
        private readonly ITeamsService teamsService;

        public TeamsController(ITeamsService teamsService)
        {
            this.teamsService = teamsService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new AllTeamsViewModel
            {
                Teams = this.teamsService.GetAll(),
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateTeamInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            this.teamsService.Create(input);

            return this.Redirect("/Teams");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            // TODO: Details section
            // 1. Details ViewModel
            // 2. Details IService
            // 3. Details Service
            // 4. Details IActionResult
            // 5. Details View

            return this.Redirect("/Teams"); ;
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            this.teamsService.DeleteById(id);

            return this.Redirect("/Teams");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}