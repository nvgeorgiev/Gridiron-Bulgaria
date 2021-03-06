﻿namespace GridironBulgaria.Web.ViewModels.Games
{
    public class GameViewModel
    {
        public int Id { get; set; }

        public string DateAndStartTime { get; set; }

        public string StadiumLocationUrl { get; set; }

        public string Format { get; set; }

        public int HomeTeamScore { get; set; }

        public int AwayTeamScore { get; set; }

        public string HomeTeamName { get; set; }

        public string HomeTeamUrl => $"/teams/details/{this.HomeTeamName.ToLower().Replace(' ', '-')}";

        public string HomeTeamLogoUrl { get; set; }

        public string HomeTeamCountry { get; set; }

        public string AwayTeamName { get; set; }

        public string AwayTeamUrl => $"/teams/details/{this.AwayTeamName.ToLower().Replace(' ', '-')}";

        public string AwayTeamLogoUrl { get; set; }

        public string AwayTeamCountry { get; set; }
    }
}
