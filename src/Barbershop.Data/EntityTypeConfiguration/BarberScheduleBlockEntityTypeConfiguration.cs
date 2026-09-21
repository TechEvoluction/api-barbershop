using Barbershop.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barbershop.Data.EntityTypeConfiguration;

internal class BarberScheduleBlockEntityTypeConfiguration : BaseEntityTypeConfiguration<BarberScheduleBlockEntity>
{
    public override void Configure(EntityTypeBuilder<BarberScheduleBlockEntity> builder)
    {
        base.Configure(builder);
        builder.ToTable("BarberScheduleBlock");

        builder.Property(x => x.BarberId)
            .IsRequired()
            .HasComment("Identificador do barbeiro");

        builder.Property(x => x.Date)
            .HasComment("Data da ausência do barbeiro");

        builder.Property(x => x.StartTime)
            .HasComment("Hora de início da ausência do barbeiro");

        builder.Property(x => x.EndTime)
            .HasComment("Hora de término da ausência do barbeiro");

        builder.Property(x => x.Reason)
            .IsRequired()
            .HasMaxLength(200)
            .HasComment("Motivo da ausência");

        builder.HasOne(x => x.Barber)
            .WithMany(x => x.ScheduleBlocks)
            .HasForeignKey(x => x.BarberId)
            .HasPrincipalKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}