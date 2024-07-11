namespace Simsa.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Simsa.Persistence.Entities;

internal class EventConfiguration : IEntityTypeConfiguration<EventEntity>
{
    public void Configure(EntityTypeBuilder<EventEntity> builder)
    {
        builder.ToTable("Event");
        builder.Property(e => e.Name)
            .HasMaxLength(ConfigurationConstants.MediumTextMaxLength)
            .IsRequired();
        builder.Property(e => e.Description)
            .HasMaxLength(ConfigurationConstants.LongTextMaxLength);
        builder.Property(e => e.StartDate)
            .IsRequired();
    }
}