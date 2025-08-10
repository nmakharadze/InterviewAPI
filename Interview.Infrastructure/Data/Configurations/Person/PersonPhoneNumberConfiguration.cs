using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Interview.Domain.Entities.Person;
using Interview.Infrastructure.Data.Configurations.Base;
using Interview.Domain.ValueObjects;

namespace Interview.Infrastructure.Data.Configurations.Person
{
    public class PersonPhoneNumberConfiguration : BaseEntityConfiguration<PersonPhoneNumber>
    {
        public override void Configure(EntityTypeBuilder<PersonPhoneNumber> builder)
        {
            base.Configure(builder);
            
            builder.ToTable("PersonPhoneNumbers", "Person");
            
            // Primary Key
            builder.HasKey(e => e.Id);
            
            // Value Objects
            builder.Property(e => e.Number)
                .HasConversion(
                    v => v.Value,
                    v => PhoneNumber.Create(v)
                )
                .IsRequired()
                .HasMaxLength(50);
            
            // Foreign Keys
            builder.Property(e => e.PersonId)
                .IsRequired();
                
            builder.Property(e => e.PhoneTypeId)
                .IsRequired();
            
            // Relationships
            builder.HasOne(e => e.Person)
                .WithMany(e => e.PhoneNumbers)
                .HasForeignKey(e => e.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.HasOne(e => e.PhoneType)
                .WithMany()
                .HasForeignKey(e => e.PhoneTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
