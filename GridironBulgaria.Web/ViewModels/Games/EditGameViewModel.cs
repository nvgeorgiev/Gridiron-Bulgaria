namespace GridironBulgaria.Web.ViewModels.Games
{
    using System.ComponentModel.DataAnnotations;

    public class EditGameViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Дата и Час (01/02/2020 - 14:00 Часа/КРАЙ)")]
        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        public string DateAndStartTime { get; set; }

        [Display(Name = "Линк към локацията на Стадиона")]
        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        public string StadiumLocationUrl { get; set; }

        [Display(Name = "ДОМАКИНИ - Отбелязани точки")]
        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        [Range(0, int.MaxValue, ErrorMessage = "Отбелязаните точки не може да са отрицателни.")]
        public int HomeTeamScore { get; set; }

        [Display(Name = "ГОСТИ - Отбелязани точки")]
        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        [Range(0, int.MaxValue, ErrorMessage = "Отбелязаните точки не може да са отрицателни.")]
        public int AwayTeamScore { get; set; }
    }
}
