using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barbershop.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyIdSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarberScheduleBlock_Barber_BarberId",
                table: "BarberScheduleBlock");

            migrationBuilder.DropForeignKey(
                name: "FK_BarberWorkday_Barber_BarberId",
                table: "BarberWorkday");

            migrationBuilder.DropForeignKey(
                name: "FK_Scheduling_Barber_BarberId",
                table: "Scheduling");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Service",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                comment: "Identificador único da entidade",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "Identificador único da entidade");

            migrationBuilder.AlterColumn<string>(
                name: "BarberId",
                table: "Scheduling",
                type: "text",
                nullable: false,
                comment: "Identificador do barbeiro",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "Identificador do barbeiro");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Scheduling",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                comment: "Identificador único da entidade",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "Identificador único da entidade");

            migrationBuilder.AlterColumn<string>(
                name: "BarberId",
                table: "BarberWorkday",
                type: "text",
                nullable: false,
                comment: "Identificador do barbeiro",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "Identificador do barbeiro");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "BarberWorkday",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                comment: "Identificador único da entidade",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "Identificador único da entidade");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "BarbershopOperation",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                comment: "Identificador único da entidade",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "Identificador único da entidade");

            migrationBuilder.AlterColumn<string>(
                name: "BarberId",
                table: "BarberScheduleBlock",
                type: "text",
                nullable: false,
                comment: "Identificador do barbeiro",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "Identificador do barbeiro");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "BarberScheduleBlock",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                comment: "Identificador único da entidade",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "Identificador único da entidade");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Barber",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                comment: "Identificador único da entidade",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "Identificador único da entidade");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Barber_UserId",
                table: "Barber",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BarberScheduleBlock_Barber_BarberId",
                table: "BarberScheduleBlock",
                column: "BarberId",
                principalTable: "Barber",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BarberWorkday_Barber_BarberId",
                table: "BarberWorkday",
                column: "BarberId",
                principalTable: "Barber",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Scheduling_Barber_BarberId",
                table: "Scheduling",
                column: "BarberId",
                principalTable: "Barber",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarberScheduleBlock_Barber_BarberId",
                table: "BarberScheduleBlock");

            migrationBuilder.DropForeignKey(
                name: "FK_BarberWorkday_Barber_BarberId",
                table: "BarberWorkday");

            migrationBuilder.DropForeignKey(
                name: "FK_Scheduling_Barber_BarberId",
                table: "Scheduling");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Barber_UserId",
                table: "Barber");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Service",
                type: "uuid",
                nullable: false,
                comment: "Identificador único da entidade",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()",
                oldComment: "Identificador único da entidade");

            migrationBuilder.AlterColumn<Guid>(
                name: "BarberId",
                table: "Scheduling",
                type: "uuid",
                nullable: false,
                comment: "Identificador do barbeiro",
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "Identificador do barbeiro");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Scheduling",
                type: "uuid",
                nullable: false,
                comment: "Identificador único da entidade",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()",
                oldComment: "Identificador único da entidade");

            migrationBuilder.AlterColumn<Guid>(
                name: "BarberId",
                table: "BarberWorkday",
                type: "uuid",
                nullable: false,
                comment: "Identificador do barbeiro",
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "Identificador do barbeiro");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "BarberWorkday",
                type: "uuid",
                nullable: false,
                comment: "Identificador único da entidade",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()",
                oldComment: "Identificador único da entidade");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "BarbershopOperation",
                type: "uuid",
                nullable: false,
                comment: "Identificador único da entidade",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()",
                oldComment: "Identificador único da entidade");

            migrationBuilder.AlterColumn<Guid>(
                name: "BarberId",
                table: "BarberScheduleBlock",
                type: "uuid",
                nullable: false,
                comment: "Identificador do barbeiro",
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "Identificador do barbeiro");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "BarberScheduleBlock",
                type: "uuid",
                nullable: false,
                comment: "Identificador único da entidade",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()",
                oldComment: "Identificador único da entidade");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Barber",
                type: "uuid",
                nullable: false,
                comment: "Identificador único da entidade",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()",
                oldComment: "Identificador único da entidade");

            migrationBuilder.AddForeignKey(
                name: "FK_BarberScheduleBlock_Barber_BarberId",
                table: "BarberScheduleBlock",
                column: "BarberId",
                principalTable: "Barber",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BarberWorkday_Barber_BarberId",
                table: "BarberWorkday",
                column: "BarberId",
                principalTable: "Barber",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Scheduling_Barber_BarberId",
                table: "Scheduling",
                column: "BarberId",
                principalTable: "Barber",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
