using Microsoft.EntityFrameworkCore;
using Interview.Domain.Entities.Dictionary;
using Interview.Infrastructure.Data.Configurations.Base;

namespace Interview.Infrastructure.Data.Configurations.Dictionary
{
    public class GenderConfiguration : DictionaryBaseConfiguration<Gender>
    {
        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Gender> builder)
        {
            base.Configure(builder);
            
            builder.ToTable("Genders", "Dictionary");
        }
    }
}
