using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barbershop.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSchemaService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Identificador único da entidade"),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, comment: "Nome do serviço"),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false, comment: "Descrição do serviço"),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false, comment: "Preço do serviço"),
                    Duration = table.Column<int>(type: "integer", nullable: false, comment: "Duração do serviço"),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false, comment: "Indica se o serviço está ativo"),
                    PromotionalPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: true, comment: "Preço promocional do serviço"),
                    PromotionalPriceEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Data de término do preço promocional do serviço"),
                    Image = table.Column<byte[]>(type: "bytea", nullable: true, comment: "Imagem do serviço"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Data e hora de criação da entidade"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Data e hora da última atualização da entidade")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Service");
        }
    }
}
