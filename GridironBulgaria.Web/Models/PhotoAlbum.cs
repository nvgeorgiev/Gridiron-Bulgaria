namespace GridironBulgaria.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PhotoAlbum
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ThumbnailPhotoUrl { get; set; }

        [Required]
        public string FacebookAlbumUrl { get; set; }

        [Required]
        public string EventDate { get; set; }

        public int HomeTeamId { get; set; }

        public Team HomeTeam { get; set; }

        public int AwayTeamId { get; set; }

        public Team AwayTeam { get; set; }
    }
}
