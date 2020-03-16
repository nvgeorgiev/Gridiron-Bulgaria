namespace GridironBulgaria.Web.ViewModels.Teams
{
    using System.Collections.Generic;

    public class AllTeamsViewModel
    {
        public int Id { get; set; }

        public IEnumerable<TeamInfoViewModel> Teams { get; set; }
    }
}