using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barbershop.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDayOfWeekInWorkday : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "LunchStarts",
                table: "BarberWorkday",
                type: "time without time zone",
                nullable: true,
                comment: "Hora de início do intervalo de almoço do barbeiro",
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone",
                oldComment: "Hora de início do intervalo de almoço do barbeiro");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "LunchEnds",
                table: "BarberWorkday",
                type: "time without time zone",
                nullable: true,
                comment: "Hora de término do intervalo de almoço do barbeiro",
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone",
                oldComment: "Hora de término do intervalo de almoço do barbeiro");

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "BarberWorkday",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                comment: "Dia da semana de trabalho do barbeiro");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "BarberWorkday");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "LunchStarts",
                table: "BarberWorkday",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0),
                comment: "Hora de início do intervalo de almoço do barbeiro",
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone",
                oldNullable: true,
                oldComment: "Hora de início do intervalo de almoço do barbeiro");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "LunchEnds",
                table: "BarberWorkday",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0),
                comment: "Hora de término do intervalo de almoço do barbeiro",
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone",
                oldNullable: true,
                oldComment: "Hora de término do intervalo de almoço do barbeiro");
        }
    }
}
