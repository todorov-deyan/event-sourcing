using EventSourcing.Api.Aggregates.CustomEs.Repository.Entities;

using Microsoft.EntityFrameworkCore;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseLazyLoadingProxies();
        //}
    }
}
