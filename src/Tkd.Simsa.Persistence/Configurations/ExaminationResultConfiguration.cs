using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tkd.Simsa.Persistence.Entities;

namespace Tkd.Simsa.Persistence.Configurations;

/// <summary>
/// Entity Framework configuration for ExaminationResultEntity.
/// </summary>
internal class ExaminationResultConfiguration : IEntityTypeConfiguration<ExaminationResultEntity>
{
    public void Configure(EntityTypeBuilder<ExaminationResultEntity> builder)
    {
        builder.ToTable("ExaminationResults");
        
        builder.HasKey(er => er.Id);
        
        builder.Property(er => er.ParticipantId)
            .IsRequired();
            
        builder.Property(er => er.ExaminationId)
            .IsRequired();
            
        builder.Property(er => er.ResultsJson)
            .HasColumnType("TEXT")
            .IsRequired();
            
        builder.Property(er => er.LastUpdated)
            .IsRequired();
            
        // Configure relationship with Event (Examination)
        builder.HasOne(er => er.Examination)
            .WithMany(e => e.ExaminationResults)
            .HasForeignKey(er => er.ExaminationId)
            .OnDelete(DeleteBehavior.Cascade);
            
        // Configure relationship with Examination
        builder.HasOne(er => er.ExaminationEntity)
            .WithMany(e => e.ExaminationResults)
            .HasForeignKey(er => er.ExaminationId)
            .OnDelete(DeleteBehavior.NoAction); // Avoid multiple cascade paths
            
        // Create unique constraint for participant-examination combination
        builder.HasIndex(er => new { er.ExaminationId, er.ParticipantId })
            .IsUnique()
            .HasDatabaseName("IX_ExaminationResults_Examination_Participant");
            
        // Create index for efficient querying by examination
        builder.HasIndex(er => er.ExaminationId)
            .HasDatabaseName("IX_ExaminationResults_Examination");
    }
}
