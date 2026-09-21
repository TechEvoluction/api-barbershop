using Barbershop.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barbershop.Data.EntityTypeConfiguration;

internal class BaseEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("gen_random_uuid()")
            .HasComment("Identificador único da entidade");

        builder.Property(e => e.CreatedAt)
            .HasComment("Data e hora de criação da entidade");

        builder.Property(e => e.UpdatedAt)
            .HasComment("Data e hora da última atualização da entidade");
    }
}