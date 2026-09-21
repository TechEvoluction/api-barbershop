using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barbershop.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDateScheduleBlock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "StartTime",
                table: "BarberScheduleBlock",
                type: "time without time zone",
                nullable: true,
                comment: "Hora de início da ausência do barbeiro",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldComment: "Hora de início da ausência do barbeiro");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "EndTime",
                table: "BarberScheduleBlock",
                type: "time without time zone",
                nullable: true,
                comment: "Hora de término da ausência do barbeiro",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldComment: "Hora de término da ausência do barbeiro");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "BarberScheduleBlock",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                comment: "Data da ausência do barbeiro");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "BarberScheduleBlock");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "BarberScheduleBlock",
                type: "timestamp with time zone",
                nullable: true,
                comment: "Hora de início da ausência do barbeiro",
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone",
                oldNullable: true,
                oldComment: "Hora de início da ausência do barbeiro");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "BarberScheduleBlock",
                type: "timestamp with time zone",
                nullable: true,
                comment: "Hora de término da ausência do barbeiro",
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone",
                oldNullable: true,
                oldComment: "Hora de término da ausência do barbeiro");
        }
    }
}
