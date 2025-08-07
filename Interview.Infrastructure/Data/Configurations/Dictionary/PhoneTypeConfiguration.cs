using Microsoft.EntityFrameworkCore;
using Interview.Domain.Entities.Dictionary;
using Interview.Infrastructure.Data.Configurations.Base;

namespace Interview.Infrastructure.Data.Configurations.Dictionary
{
    public class PhoneTypeConfiguration : DictionaryBaseConfiguration<PhoneType>
    {
        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PhoneType> builder)
        {
            base.Configure(builder);
            
            builder.ToTable("PhoneTypes", "Dictionary");
        }
    }
}
