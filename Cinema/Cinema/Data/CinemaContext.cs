using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cinema.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Cinema.Data
{
    public class CinemaContext : IdentityDbContext
    {
        public CinemaContext (DbContextOptions<CinemaContext> options)
            : base(options)
        {
        }

        public DbSet<Cinema.Models.Movie> Movie { get; set; } = default!;
        public DbSet<Cinema.Models.Director> Director { get; set; } = default!;
        public DbSet<Cinema.Models.Category> Category { get; set; } = default!;
        public DbSet<Cinema.Models.Showing> Showing { get; set; } = default!;
        public DbSet<Cinema.Areas.Identity.User> User { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasOne(b => b.Director)
                .WithMany(a => a.Movies)
                .HasForeignKey(b => b.DirectorId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(b => b.Category)
                .WithMany(a => a.Movies)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(b => b.Showings)
                .WithOne(a => a.Movie)
                .HasForeignKey(b => b.MovieId)
                .OnDelete(DeleteBehavior.Restrict);
            });
                

            base.OnModelCreating(modelBuilder);
        }
    }
}
