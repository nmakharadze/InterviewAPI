using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Interview.Domain.Entities.Person;
using Interview.Infrastructure.Data.Configurations.Base;

namespace Interview.Infrastructure.Data.Configurations.Person
{
    public class PersonRelationConfiguration : BaseEntityConfiguration<PersonRelation>
    {
        public override void Configure(EntityTypeBuilder<PersonRelation> builder)
        {
            base.Configure(builder);
            
            builder.ToTable("PersonRelations", "Person");
            
            // Relationships
            builder.HasOne(e => e.Person)
                .WithMany(e => e.Relations)
                .HasForeignKey(e => e.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.HasOne(e => e.RelatedPerson)
                .WithMany()
                .HasForeignKey(e => e.RelatedPersonId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasOne(e => e.RelationType)
                .WithMany()
                .HasForeignKey(e => e.RelationTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
