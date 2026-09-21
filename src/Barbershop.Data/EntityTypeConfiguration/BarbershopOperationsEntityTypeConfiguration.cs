using Barbershop.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barbershop.Data.EntityTypeConfiguration;

internal class BarbershopOperationEntityTypeConfiguration : BaseEntityTypeConfiguration<BarbershopOperationEntity>
{
    public override void Configure(EntityTypeBuilder<BarbershopOperationEntity> builder)
    {
        base.Configure(builder);
        builder.ToTable("BarbershopOperation");

        builder.Property(x => x.DayOfWeek)
            .IsRequired()
            .HasComment("Dia da semana de operação");

        builder.Property(x => x.OpeningHours)
            .HasComment("Hora de início da operação");

        builder.Property(x => x.ClosingTime)
            .IsRequired()
            .HasComment("Hora de término da operação");
    }
}