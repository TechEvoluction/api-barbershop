using Barbershop.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barbershop.Data.EntityTypeConfiguration;

internal class BarberEntityTypeConfiguration : BaseEntityTypeConfiguration<BarberEntity>
{
    public override void Configure(EntityTypeBuilder<BarberEntity> builder)
    {
        base.Configure(builder);
        builder.ToTable("Barber");

        builder.Property(x => x.UserId)
            .IsRequired()
            .HasComment("Identificador do barbeiro");

        builder.Property(x => x.Biography)
            .HasComment("Biografia do barbeiro");

        builder.Property(x => x.AvailableToAssist)
            .HasComment("Barbeiro disponível para atendimento");

        builder.HasMany(x => x.Schedules)
            .WithOne(x => x.Barber)
            .HasForeignKey(x => x.BarberId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Workdays)
            .WithOne(x => x.Barber)
            .HasForeignKey(x => x.BarberId)
            .HasPrincipalKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.ScheduleBlocks)
            .WithOne(x => x.Barber)
            .HasForeignKey(x => x.BarberId)
            .HasPrincipalKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}