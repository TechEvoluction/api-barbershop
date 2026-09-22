using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barbershop.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDateInBarbershopOperation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "ClosingTime",
                table: "BarbershopOperation",
                type: "time without time zone",
                nullable: true,
                comment: "Hora de término da operação",
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone",
                oldComment: "Hora de término da operação");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "BarbershopOperation",
                type: "date",
                nullable: true,
                comment: "Data específica da operação");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "BarbershopOperation");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "ClosingTime",
                table: "BarbershopOperation",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0),
                comment: "Hora de término da operação",
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone",
                oldNullable: true,
                oldComment: "Hora de término da operação");
        }
    }
}
