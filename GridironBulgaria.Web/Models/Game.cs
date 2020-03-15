namespace GridironBulgaria.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string DateAndStartTime { get; set; }

        [Required]
        public string StadiumLocationUrl { get; set; }

        [Required]
        public string Format { get; set; }

        public int HomeTeamScore { get; set; }

        public int AwayTeamScore { get; set; }

        public int HomeTeamId { get; set; }

        public Team HomeTeam { get; set; }

        public int AwayTeamId { get; set; }

        public Team AwayTeam { get; set; }
    }
}
