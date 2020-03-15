namespace GridironBulgaria.Web.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Team
    {
        public Team()
        {
            this.HomeGames = new HashSet<Game>();
            this.AwayGames = new HashSet<Game>();
            this.HomePhotoAlbums = new HashSet<PhotoAlbum>();
            this.AwayPhotoAlbums = new HashSet<PhotoAlbum>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public string CoverPhotoUrl { get; set; }

        public string CoachName { get; set; }

        public string TrainingsDescription { get; set; }

        public string ContactUrl { get; set; }

        public int TownId { get; set; }

        public Town Town { get; set; }

        public virtual ICollection<Game> HomeGames { get; set; }

        public virtual ICollection<Game> AwayGames { get; set; }

        public virtual ICollection<PhotoAlbum> HomePhotoAlbums { get; set; }

        public virtual ICollection<PhotoAlbum> AwayPhotoAlbums { get; set; }
    }
}
