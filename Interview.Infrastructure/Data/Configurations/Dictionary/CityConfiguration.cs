using Microsoft.EntityFrameworkCore;
using Interview.Domain.Entities.Dictionary;
using Interview.Infrastructure.Data.Configurations.Base;

namespace Interview.Infrastructure.Data.Configurations.Dictionary
{
    public class CityConfiguration : DictionaryBaseConfiguration<City>
    {
        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<City> builder)
        {
            base.Configure(builder);
            
            builder.ToTable("Cities", "Dictionary");
        }
    }
}
