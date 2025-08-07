using Microsoft.EntityFrameworkCore;
using Interview.Domain.Entities.Dictionary;
using Interview.Infrastructure.Data.Configurations.Base;

namespace Interview.Infrastructure.Data.Configurations.Dictionary
{
    public class RelationTypeConfiguration : DictionaryBaseConfiguration<RelationType>
    {
        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<RelationType> builder)
        {
            base.Configure(builder);
            
            builder.ToTable("RelationTypes", "Dictionary");
        }
    }
}
