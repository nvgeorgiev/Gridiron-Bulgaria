namespace GridironBulgaria.Web.Data.Configurations
{
    using GridironBulgaria.Web.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PhotoAlbumConfiguration : IEntityTypeConfiguration<PhotoAlbum>
    {
        public void Configure(EntityTypeBuilder<PhotoAlbum> entity)
        {
            entity
                .HasOne(pa => pa.HomeTeam)
                .WithMany(t => t.HomePhotoAlbums)
                .HasForeignKey(pa => pa.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasOne(pa => pa.AwayTeam)
                .WithMany(t => t.AwayPhotoAlbums)
                .HasForeignKey(pa => pa.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
