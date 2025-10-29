using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiChaRifaYan.Migrations
{
    /// <inheritdoc />
    public partial class ajustedatahora : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataHora",
                table: "Sorteio",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2025, 10, 29, 9, 40, 17, 3, DateTimeKind.Utc).AddTicks(321),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 10, 25, 13, 26, 30, 400, DateTimeKind.Utc).AddTicks(6089));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataHoraCadastro",
                table: "Participante",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 10, 25, 13, 26, 30, 400, DateTimeKind.Utc).AddTicks(5232));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataHora",
                table: "Sorteio",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2025, 10, 25, 13, 26, 30, 400, DateTimeKind.Utc).AddTicks(6089),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 10, 29, 9, 40, 17, 3, DateTimeKind.Utc).AddTicks(321));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataHoraCadastro",
                table: "Participante",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2025, 10, 25, 13, 26, 30, 400, DateTimeKind.Utc).AddTicks(5232),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "NOW()");
        }
    }
}
