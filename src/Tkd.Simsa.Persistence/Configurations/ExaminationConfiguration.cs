using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tkd.Simsa.Persistence.Entities;

namespace Tkd.Simsa.Persistence.Configurations;

/// <summary>
/// Entity Framework configuration for ExaminationEntity.
/// </summary>
internal class ExaminationConfiguration : IEntityTypeConfiguration<ExaminationEntity>
{
    public void Configure(EntityTypeBuilder<ExaminationEntity> builder)
    {
        builder.ToTable("Examinations");
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Name)
            .HasMaxLength(ConfigurationConstants.MediumTextMaxLength)
            .IsRequired();
            
        builder.Property(e => e.Description)
            .HasMaxLength(ConfigurationConstants.LongTextMaxLength);
            
        builder.Property(e => e.StartDate)
            .IsRequired();
            
        builder.Property(e => e.ParticipationData)
            .HasColumnType("TEXT")
            .IsRequired();
            
        // Configure examination progress
        builder.Property(e => e.ProgressJson)
            .HasColumnType("TEXT")
            .IsRequired(false);
            
        // Configure navigation properties for disciplines and results
        builder.HasMany(e => e.Disciplines)
            .WithOne(d => d.Examination)
            .HasForeignKey(d => d.ExaminationId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasMany(e => e.ExaminationResults)
            .WithOne(er => er.ExaminationEntity)
            .HasForeignKey(er => er.ExaminationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
