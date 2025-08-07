using Microsoft.EntityFrameworkCore;
using Interview.Domain.Entities.Dictionary;
using Interview.Domain.Entities.Person;

namespace Interview.Infrastructure.Data
{
    public class InterviewDbContext : DbContext
    {
        public InterviewDbContext(DbContextOptions<InterviewDbContext> options) : base(options)
        {
        }

        // Dictionary Entities
        public DbSet<Gender> Genders { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<PhoneType> PhoneTypes { get; set; }
        public DbSet<RelationType> RelationTypes { get; set; }

        // Person Entities
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonPhoneNumber> PersonPhoneNumbers { get; set; }
        public DbSet<PersonRelation> PersonRelations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Create schemas
            modelBuilder.HasDefaultSchema("dbo");
            
            // Apply all configurations from current assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InterviewDbContext).Assembly);
        }
    }
}
