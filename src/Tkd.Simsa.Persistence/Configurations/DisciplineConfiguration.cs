using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tkd.Simsa.Persistence.Entities;

namespace Tkd.Simsa.Persistence.Configurations;

/// <summary>
/// Entity Framework configuration for DisciplineEntity.
/// </summary>
internal class DisciplineConfiguration : IEntityTypeConfiguration<DisciplineEntity>
{
    public void Configure(EntityTypeBuilder<DisciplineEntity> builder)
    {
        builder.ToTable("Disciplines");
        
        builder.HasKey(d => d.Id);
        
        builder.Property(d => d.Name)
            .HasMaxLength(ConfigurationConstants.MediumTextMaxLength)
            .IsRequired();
            
        builder.Property(d => d.Description)
            .HasMaxLength(ConfigurationConstants.LongTextMaxLength)
            .IsRequired();
            
        builder.Property(d => d.Type)
            .HasConversion<string>()
            .HasMaxLength(ConfigurationConstants.ShortTextMaxLength)
            .IsRequired();
            
        builder.Property(d => d.Order)
            .IsRequired();
            
        builder.Property(d => d.EventId)
            .IsRequired();
            
        // Configure relationship with Event
        builder.HasOne(d => d.Event)
            .WithMany(e => e.Disciplines)
            .HasForeignKey(d => d.EventId)
            .OnDelete(DeleteBehavior.Cascade);
            
        // Create index for efficient querying
        builder.HasIndex(d => new { d.EventId, d.Order })
            .HasDatabaseName("IX_Disciplines_Event_Order");
    }
}
