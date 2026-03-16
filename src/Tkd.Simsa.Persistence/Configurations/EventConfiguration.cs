namespace Tkd.Simsa.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Tkd.Simsa.Persistence.Entities;

/// <summary>
/// Entity configuration for EventEntity.
/// Note: Examination-specific navigation properties are configured in ExaminationConfiguration.
/// </summary>
internal class EventConfiguration : IEntityTypeConfiguration<EventEntity>
{
    public void Configure(EntityTypeBuilder<EventEntity> builder)
    {
        builder.ToTable("Event");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name)
            .HasMaxLength(ConfigurationConstants.MediumTextMaxLength)
            .IsRequired();
        builder.Property(e => e.Description)
            .HasMaxLength(ConfigurationConstants.LongTextMaxLength);
        builder.Property(e => e.StartDate)
            .IsRequired();
    }
}