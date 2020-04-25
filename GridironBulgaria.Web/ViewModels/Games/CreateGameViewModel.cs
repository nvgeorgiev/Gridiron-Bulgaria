namespace GridironBulgaria.Web.ViewModels.Games
{
    using System.ComponentModel.DataAnnotations;

    public class CreateGameViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Дата и Час (01/02/2020 - 14:00 Часа)")]
        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        public string DateAndStartTime { get; set; }

        [Display(Name = "Линк към локацията на Стадиона")]
        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        public string StadiumLocationUrl { get; set; }

        [Display(Name = "Формат на мача (Сезон 2020 / 1-ви Кръг)")]
        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        public string Format { get; set; }

        [Display(Name = "ДОМАКИНИ - Отбелязани точки")]
        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        [Range(0, int.MaxValue, ErrorMessage = "Отбелязаните точки не може да са отрицателни.")]
        public int HomeTeamScore { get; set; }

        [Display(Name = "ГОСТИ - Отбелязани точки")]
        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        [Range(0, int.MaxValue, ErrorMessage = "Отбелязаните точки не може да са отрицателни.")]
        public int AwayTeamScore { get; set; }

        [Display(Name = "Име на отбора ДОМАКИН (на Латиница)")]
        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        public string HomeTeamName { get; set; }

        [Display(Name = "Линк към логото на отбора ДОМАКИН, ако отбора не е български")]
        public string HomeTeamLogoUrl { get; set; }

        [Display(Name = "Име на отбора ГОСТ (на Латиница)")]
        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        public string AwayTeamName { get; set; }

        [Display(Name = "Линк към логото на отбора ГОСТ, ако отбора не е български")]
        public string AwayTeamLogoUrl { get; set; }
    }
}
