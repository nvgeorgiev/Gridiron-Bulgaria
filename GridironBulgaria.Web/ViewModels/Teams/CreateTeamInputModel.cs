namespace GridironBulgaria.Web.ViewModels.Teams
{
    using System.ComponentModel.DataAnnotations;

    public class CreateTeamInputModel
    {
        public int Id { get; set; }

        [Display(Name = "Име (на Латиница)")]
        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        public string Name { get; set; }

        [Display(Name = "Линк към логото на отбора")]
        public string LogoUrl { get; set; }

        [Display(Name = "Линк към отборна снимка")]
        public string CoverPhotoUrl { get; set; }

        [Display(Name = "Треньор (Име и Фамилия)")]
        public string CoachName { get; set; }

        [Display(Name = "Къде и кога тренира отбора")]
        public string TrainingsDescription { get; set; }

        [Display(Name = "Линк към фейсбук страница или сайт на отбора")]
        public string ContactUrl { get; set; }

        [Display(Name = "Държава (на Кирилица)")]
        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        public string CountryName { get; set; }

        [Display(Name = "Град (на Кирилица)")]
        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        public string TownName { get; set; }
    }
}