using Barbershop.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barbershop.Data.EntityTypeConfiguration;

internal class BarberWorkdayEntityTypeConfiguration : BaseEntityTypeConfiguration<BarberWorkdayEntity>
{
    public override void Configure(EntityTypeBuilder<BarberWorkdayEntity> builder)
    {
        base.Configure(builder);
        builder.ToTable("BarberWorkday");

        builder.Property(x => x.BarberId)
            .IsRequired()
            .HasComment("Identificador do barbeiro");

        builder.Property(x => x.DayOfWeek)
            .IsRequired()
            .HasComment("Dia da semana de trabalho do barbeiro");

        builder.Property(x => x.StartTime)
            .IsRequired()
            .HasComment("Hora de início de trabalho do barbeiro");

        builder.Property(x => x.EndTime)
            .IsRequired()
            .HasComment("Hora de término de trabalho do barbeiro");

        builder.Property(x => x.LunchStarts)
            .HasComment("Hora de início do intervalo de almoço do barbeiro");

        builder.Property(x => x.LunchEnds)
            .HasComment("Hora de término do intervalo de almoço do barbeiro");

        builder.HasOne(x => x.Barber)
            .WithMany(x => x.Workdays)
            .HasForeignKey(x => x.BarberId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}