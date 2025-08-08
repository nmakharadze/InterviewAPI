using Interview.Domain.Entities.Dictionary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Interview.Infrastructure.Data.Configurations.Dictionary
{
    /// <summary>
    /// DictionaryName entity-ის კონფიგურაცია
    /// </summary>
    public class DictionaryNameConfiguration : IEntityTypeConfiguration<DictionaryName>
    {
        public void Configure(EntityTypeBuilder<DictionaryName> builder)
        {
            // ცხრილის სახელი
            builder.ToTable("DictionaryNames", "config");
            
            // Primary Key
            builder.HasKey(x => x.Id);
            
            // Properties
            builder.Property(x => x.TableName)
                .IsRequired()
                .HasMaxLength(100);
                
            builder.Property(x => x.EntityTypeName)
                .IsRequired()
                .HasMaxLength(100);
            
            // Indexes
            builder.HasIndex(x => x.TableName)
                .IsUnique();
        }
    }
}
