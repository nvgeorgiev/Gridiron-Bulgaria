namespace GridironBulgaria.Web.ViewModels.Teams
{
    public class TeamInfoViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public string CountryName { get; set; }

        public string Url => $"/teams/{this.Name.ToLower().Replace(' ', '-')}";
    }
}