using IdealRatingTechnicalTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdealRatingTechnicalTask.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entitiy =>
            {
                entitiy.HasKey(e => e.Id);
            });
        }
    }
}
