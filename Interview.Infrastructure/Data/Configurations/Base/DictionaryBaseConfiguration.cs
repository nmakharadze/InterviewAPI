using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Interview.Domain.Entities.Base;

namespace Interview.Infrastructure.Data.Configurations.Base
{
    public abstract class DictionaryBaseConfiguration<T> : BaseEntityConfiguration<T> where T : DictionaryBase
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);
            
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
                
            builder.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValue(true);
                
            builder.HasIndex(e => e.Name)
                .IsUnique();
        }
    }
}
