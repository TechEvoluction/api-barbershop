using Barbershop.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barbershop.Data.EntityTypeConfiguration;

internal class SchedulingEntityTypeConfiguration : BaseEntityTypeConfiguration<SchedulingEntity>
{
    public override void Configure(EntityTypeBuilder<SchedulingEntity> builder)
    {
        base.Configure(builder);
        builder.ToTable("Scheduling");

        builder.Property(x => x.Date)
            .IsRequired()
            .HasComment("Data do agendamento");

        builder.Property(x => x.Hour)
            .IsRequired()
            .HasComment("Hora do agendamento");

        builder.Property(x => x.Status)
            .IsRequired()
            .HasComment("Status do agendamento");

        builder.Property(x => x.BarberId)
            .IsRequired()
            .HasComment("Identificador do barbeiro");

        builder.Property(x => x.UserId)
            .IsRequired()
            .HasComment("Identificador do usuário");

        builder.Property(x => x.ServiceId)
            .IsRequired()
            .HasComment("Identificador do serviço");

        builder.Property(x => x.Observation)
            .HasComment("Observações do cliente sobre o agendamento");
        
        builder.Property(x => x.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasComment("Preço do serviço no momento do agendamento");

        builder.HasOne(x => x.Barber)
            .WithMany(x => x.Schedules)
            .HasForeignKey(x => x.BarberId)
            .HasPrincipalKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Schedules)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Service)
            .WithMany(x => x.Schedules)
            .HasForeignKey(x => x.ServiceId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}