using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoviesStore.Models;

namespace MoviesStore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>().HasIndex(movie => movie.Name);
            modelBuilder.Entity<Movie>().Property(movie => movie.Name).IsRequired();
            modelBuilder.Entity<Movie>().Property(movie => movie.UserId).IsRequired();
            modelBuilder.Entity<Movie>().Ignore(movie => movie.PosterImg);
        }
    }
}
