using Barbershop.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barbershop.Data.EntityTypeConfiguration;

internal class ServiceEntityTypeConfiguration : BaseEntityTypeConfiguration<ServiceEntity>
{
    public override void Configure(EntityTypeBuilder<ServiceEntity> builder)
    {
        base.Configure(builder);
        builder.ToTable("Service");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50)
            .HasComment("Nome do serviço");

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(500)
            .HasComment("Descrição do serviço");

        builder.Property(x => x.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasComment("Preço do serviço");

        builder.Property(x => x.Duration)
            .IsRequired()
            .HasComment("Duração do serviço");

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(false)
            .HasComment("Indica se o serviço está ativo");

        builder.Property(x => x.PromotionalPrice)
            .HasColumnType("decimal(18,2)")
            .HasComment("Preço promocional do serviço");

        builder.Property(x => x.PromotionalPriceEndDate)
            .HasComment("Data de término do preço promocional do serviço");

        builder.Property(x => x.Image)
            .HasColumnType("bytea")
            .HasComment("Imagem do serviço");
    }
}