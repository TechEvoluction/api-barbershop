using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barbershop.Data.Migrations
{
    /// <inheritdoc />
    public partial class BarberAndBarbershopSchemas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Barber",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Identificador único da entidade"),
                    UserId = table.Column<string>(type: "text", nullable: false, comment: "Identificador do barbeiro"),
                    Biography = table.Column<string>(type: "text", nullable: true, comment: "Biografia do barbeiro"),
                    AvailableToAssist = table.Column<bool>(type: "boolean", nullable: false, comment: "Barbeiro disponível para atendimento"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Data e hora de criação da entidade"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Data e hora da última atualização da entidade")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barber", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BarbershopOperation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Identificador único da entidade"),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: false, comment: "Dia da semana de operação"),
                    OpeningHours = table.Column<TimeOnly>(type: "time without time zone", nullable: true, comment: "Hora de início da operação"),
                    ClosingTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false, comment: "Hora de término da operação"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Data e hora de criação da entidade"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Data e hora da última atualização da entidade")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarbershopOperation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BarberScheduleBlock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Identificador único da entidade"),
                    BarberId = table.Column<Guid>(type: "uuid", nullable: false, comment: "Identificador do barbeiro"),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Hora de início da ausência do barbeiro"),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Hora de término da ausência do barbeiro"),
                    Reason = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, comment: "Motivo da ausência"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Data e hora de criação da entidade"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Data e hora da última atualização da entidade")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarberScheduleBlock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BarberScheduleBlock_Barber_BarberId",
                        column: x => x.BarberId,
                        principalTable: "Barber",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BarberWorkday",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Identificador único da entidade"),
                    BarberId = table.Column<Guid>(type: "uuid", nullable: false, comment: "Identificador do barbeiro"),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false, comment: "Hora de início de trabalho do barbeiro"),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false, comment: "Hora de término de trabalho do barbeiro"),
                    LunchStarts = table.Column<TimeOnly>(type: "time without time zone", nullable: false, comment: "Hora de início do intervalo de almoço do barbeiro"),
                    LunchEnds = table.Column<TimeOnly>(type: "time without time zone", nullable: false, comment: "Hora de término do intervalo de almoço do barbeiro"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Data e hora de criação da entidade"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Data e hora da última atualização da entidade")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarberWorkday", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BarberWorkday_Barber_BarberId",
                        column: x => x.BarberId,
                        principalTable: "Barber",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Scheduling",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Identificador único da entidade"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false, comment: "Data do agendamento"),
                    Hour = table.Column<TimeOnly>(type: "time without time zone", nullable: false, comment: "Hora do agendamento"),
                    Status = table.Column<int>(type: "integer", nullable: false, comment: "Status do agendamento"),
                    BarberId = table.Column<Guid>(type: "uuid", nullable: false, comment: "Identificador do barbeiro"),
                    UserId = table.Column<string>(type: "text", nullable: false, comment: "Identificador do usuário"),
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: false, comment: "Identificador do serviço"),
                    Observation = table.Column<string>(type: "text", nullable: true, comment: "Observações do cliente sobre o agendamento"),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false, comment: "Preço do serviço no momento do agendamento"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Data e hora de criação da entidade"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Data e hora da última atualização da entidade")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scheduling", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scheduling_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Scheduling_Barber_BarberId",
                        column: x => x.BarberId,
                        principalTable: "Barber",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Scheduling_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BarberScheduleBlock_BarberId",
                table: "BarberScheduleBlock",
                column: "BarberId");

            migrationBuilder.CreateIndex(
                name: "IX_BarberWorkday_BarberId",
                table: "BarberWorkday",
                column: "BarberId");

            migrationBuilder.CreateIndex(
                name: "IX_Scheduling_BarberId",
                table: "Scheduling",
                column: "BarberId");

            migrationBuilder.CreateIndex(
                name: "IX_Scheduling_ServiceId",
                table: "Scheduling",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Scheduling_UserId",
                table: "Scheduling",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BarberScheduleBlock");

            migrationBuilder.DropTable(
                name: "BarbershopOperation");

            migrationBuilder.DropTable(
                name: "BarberWorkday");

            migrationBuilder.DropTable(
                name: "Scheduling");

            migrationBuilder.DropTable(
                name: "Barber");
        }
    }
}
