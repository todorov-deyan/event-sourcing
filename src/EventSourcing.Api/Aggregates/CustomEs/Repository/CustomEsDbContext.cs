using EventSourcing.Api.Aggregates.CustomEs.Repository.Entities;

using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EventSourcing.Api.Aggregates.CustomEs.Repository
{
    public class CustomEsDbContext : DbContext
    {
        public CustomEsDbContext(DbContextOptions<CustomEsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CustomStream> Streams { get; set; }

        public virtual DbSet<CustomEvent> Events { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomStream>()
                        .HasMany(c => c.Events)
                        .WithOne(e => e.Stream);

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseLazyLoadingProxies();
        //}
    }
}
