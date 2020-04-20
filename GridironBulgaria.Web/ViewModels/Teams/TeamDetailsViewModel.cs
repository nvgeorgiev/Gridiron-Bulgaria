namespace GridironBulgaria.Web.ViewModels.Teams
{
    using GridironBulgaria.Web.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TeamDetailsViewModel
    {
        public int Id { get; set; }

        public string LogoUrl { get; set; }

        public string Name { get; set; }

        public string CoverPhotoUrl { get; set; }

        [Display(Name = "Треньор:")]
        public string CoachName { get; set; }

        [Display(Name = "Тренировки:")]
        public string TrainingsDescription { get; set; }

        [Display(Name = "Контакти:")]
        public string ContactUrl { get; set; }

        [Display(Name = "Изиграни мачове (от 2020г. до сега):")]
        public int GamesPlayedCounter { get; set; }

        [Display(Name = "Галерия:")]
        public virtual IEnumerable<PhotoAlbum> TeamPhotoAlbums { get; set; }

        public string TeamGamesFilter => $"/games?search={this.Name.ToLower().Replace(' ', '+')}";
    }
}
