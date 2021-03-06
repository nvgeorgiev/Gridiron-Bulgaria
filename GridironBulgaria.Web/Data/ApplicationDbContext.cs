﻿namespace GridironBulgaria.Web.Data
{
    using GridironBulgaria.Web.Data.Configurations;
    using GridironBulgaria.Web.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<PhotoAlbum> PhotoAlbums { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TownConfiguration());

            modelBuilder.ApplyConfiguration(new TeamConfiguration());

            modelBuilder.ApplyConfiguration(new GameConfiguration());

            modelBuilder.ApplyConfiguration(new PhotoAlbumConfiguration());
        }
    }
}
