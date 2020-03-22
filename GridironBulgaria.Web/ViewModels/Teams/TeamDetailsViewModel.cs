using GridironBulgaria.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GridironBulgaria.Web.ViewModels.Teams
{
    public class TeamDetailsViewModel
    {
        public int Id { get; set; }

        public string LogoUrl { get; set; }

        public string Name { get; set; }

        public string CoverPhotoUrl { get; set; }

        public string CoachName { get; set; }

        public string TrainingsDescription { get; set; }

        public string ContactUrl { get; set; }

        public int GamesPlayedCounter { get; set; }

        public virtual ICollection<PhotoAlbum> TeamPhotoAlbums { get; set; }
    }
}
