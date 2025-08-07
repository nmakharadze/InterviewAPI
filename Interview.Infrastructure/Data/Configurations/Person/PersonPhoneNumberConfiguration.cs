using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Interview.Domain.Entities.Person;
using Interview.Infrastructure.Data.Configurations.Base;

namespace Interview.Infrastructure.Data.Configurations.Person
{
    public class PersonPhoneNumberConfiguration : BaseEntityConfiguration<PersonPhoneNumber>
    {
        public override void Configure(EntityTypeBuilder<PersonPhoneNumber> builder)
        {
            base.Configure(builder);
            
            builder.ToTable("PersonPhoneNumbers", "Person");
            
            // Value Objects
            builder.OwnsOne(e => e.Number, phoneNumber =>
            {
                phoneNumber.Property(p => p.Value)
                    .HasColumnName("Number")
                    .IsRequired()
                    .HasMaxLength(50);
            });
            
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
