using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Interview.Domain.Entities.Person;
using Interview.Infrastructure.Data.Configurations.Base;

namespace Interview.Infrastructure.Data.Configurations.Person
{
    public class PersonConfiguration : BaseEntityConfiguration<Interview.Domain.Entities.Person.Person>
    {
        public override void Configure(EntityTypeBuilder<Interview.Domain.Entities.Person.Person> builder)
        {
            base.Configure(builder);
            
            builder.ToTable("Persons", "Person");
            
            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);
                
            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);
                
            builder.Property(e => e.BirthDate)
                .IsRequired();
                
            builder.Property(e => e.ImagePath)
                .HasMaxLength(500);
                
            // Value Objects
            builder.OwnsOne(e => e.PersonalNumber, personalNumber =>
            {
                personalNumber.Property(p => p.Value)
                    .HasColumnName("PersonalNumber")
                    .IsRequired()
                    .HasMaxLength(11);
            });
                
            // Relationships
            builder.HasOne(e => e.Gender)
                .WithMany()
                .HasForeignKey(e => e.GenderId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasOne(e => e.City)
                .WithMany()
                .HasForeignKey(e => e.CityId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasMany(e => e.PhoneNumbers)
                .WithOne(e => e.Person)
                .HasForeignKey(e => e.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.HasMany(e => e.Relations)
                .WithOne(e => e.Person)
                .HasForeignKey(e => e.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
