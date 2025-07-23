namespace Tkd.Simsa.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Tkd.Simsa.Persistence.Entities;

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
            
        // Configure examination-specific properties
        builder.Property(e => e.ExaminationProgressJson)
            .HasColumnType("TEXT")
            .IsRequired(false);
            
        // Configure navigation properties for disciplines and results
        builder.HasMany(e => e.Disciplines)
            .WithOne(d => d.Event)
            .HasForeignKey(d => d.EventId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasMany(e => e.ExaminationResults)
            .WithOne(er => er.Examination)
            .HasForeignKey(er => er.ExaminationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}