namespace GridironBulgaria.Web.ViewModels.PhotoAlbums
{
    using System.ComponentModel.DataAnnotations;

    public class EditPhotoAlbumViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Заглавие (Sofia Bears vs. Sofia Bears)")]
        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        public string Title { get; set; }

        [Display(Name = "Линк към Thumbnail снимка")]
        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        public string ThumbnailPhotoUrl { get; set; }

        [Display(Name = "Линк към фейсбук албума със снимки от мача")]
        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        public string FacebookAlbumUrl { get; set; }

        [Display(Name = "Дата на събитието (01/12/2020)")]
        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        public string EventDate { get; set; }

        [Display(Name = "Име на отбора домакин (на Латиница)")]
        public string HomeTeamName { get; set; }

        [Display(Name = "Име на отбора гост (на Латиница)")]
        public string AwayTeamName { get; set; }
    }
}
