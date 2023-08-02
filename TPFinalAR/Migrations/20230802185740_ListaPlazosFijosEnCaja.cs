using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPFinalAR.Migrations
{
    /// <inheritdoc />
    public partial class ListaPlazosFijosEnCaja : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CajaDeAhorro_id_caja",
                table: "Plazo_fijo",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Plazo_fijo",
                keyColumn: "_id_plazoFijo",
                keyValue: 1,
                columns: new[] { "CajaDeAhorro_id_caja", "_fechaFin", "_fechaIni" },
                values: new object[] { null, new DateTime(2023, 9, 2, 15, 57, 40, 204, DateTimeKind.Local).AddTicks(6857), new DateTime(2023, 8, 2, 15, 57, 40, 204, DateTimeKind.Local).AddTicks(6835) });

            migrationBuilder.UpdateData(
                table: "Plazo_fijo",
                keyColumn: "_id_plazoFijo",
                keyValue: 2,
                columns: new[] { "CajaDeAhorro_id_caja", "_fechaFin", "_fechaIni" },
                values: new object[] { null, new DateTime(2023, 9, 2, 15, 57, 40, 204, DateTimeKind.Local).AddTicks(6866), new DateTime(2023, 8, 2, 15, 57, 40, 204, DateTimeKind.Local).AddTicks(6866) });

            migrationBuilder.UpdateData(
                table: "Plazo_fijo",
                keyColumn: "_id_plazoFijo",
                keyValue: 3,
                columns: new[] { "CajaDeAhorro_id_caja", "_fechaFin", "_fechaIni" },
                values: new object[] { null, new DateTime(2023, 9, 2, 15, 57, 40, 204, DateTimeKind.Local).AddTicks(6869), new DateTime(2023, 8, 2, 15, 57, 40, 204, DateTimeKind.Local).AddTicks(6868) });

            migrationBuilder.UpdateData(
                table: "Plazo_fijo",
                keyColumn: "_id_plazoFijo",
                keyValue: 4,
                columns: new[] { "CajaDeAhorro_id_caja", "_fechaFin", "_fechaIni" },
                values: new object[] { null, new DateTime(2023, 9, 2, 15, 57, 40, 204, DateTimeKind.Local).AddTicks(6870), new DateTime(2023, 8, 2, 15, 57, 40, 204, DateTimeKind.Local).AddTicks(6870) });

            migrationBuilder.CreateIndex(
                name: "IX_Plazo_fijo_CajaDeAhorro_id_caja",
                table: "Plazo_fijo",
                column: "CajaDeAhorro_id_caja");

            migrationBuilder.AddForeignKey(
                name: "FK_Plazo_fijo_Caja_ahorro_CajaDeAhorro_id_caja",
                table: "Plazo_fijo",
                column: "CajaDeAhorro_id_caja",
                principalTable: "Caja_ahorro",
                principalColumn: "_id_caja");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plazo_fijo_Caja_ahorro_CajaDeAhorro_id_caja",
                table: "Plazo_fijo");

            migrationBuilder.DropIndex(
                name: "IX_Plazo_fijo_CajaDeAhorro_id_caja",
                table: "Plazo_fijo");

            migrationBuilder.DropColumn(
                name: "CajaDeAhorro_id_caja",
                table: "Plazo_fijo");

            migrationBuilder.UpdateData(
                table: "Plazo_fijo",
                keyColumn: "_id_plazoFijo",
                keyValue: 1,
                columns: new[] { "_fechaFin", "_fechaIni" },
                values: new object[] { new DateTime(2023, 9, 1, 16, 10, 57, 870, DateTimeKind.Local).AddTicks(3055), new DateTime(2023, 8, 1, 16, 10, 57, 870, DateTimeKind.Local).AddTicks(3040) });

            migrationBuilder.UpdateData(
                table: "Plazo_fijo",
                keyColumn: "_id_plazoFijo",
                keyValue: 2,
                columns: new[] { "_fechaFin", "_fechaIni" },
                values: new object[] { new DateTime(2023, 9, 1, 16, 10, 57, 870, DateTimeKind.Local).AddTicks(3066), new DateTime(2023, 8, 1, 16, 10, 57, 870, DateTimeKind.Local).AddTicks(3066) });

            migrationBuilder.UpdateData(
                table: "Plazo_fijo",
                keyColumn: "_id_plazoFijo",
                keyValue: 3,
                columns: new[] { "_fechaFin", "_fechaIni" },
                values: new object[] { new DateTime(2023, 9, 1, 16, 10, 57, 870, DateTimeKind.Local).AddTicks(3070), new DateTime(2023, 8, 1, 16, 10, 57, 870, DateTimeKind.Local).AddTicks(3069) });

            migrationBuilder.UpdateData(
                table: "Plazo_fijo",
                keyColumn: "_id_plazoFijo",
                keyValue: 4,
                columns: new[] { "_fechaFin", "_fechaIni" },
                values: new object[] { new DateTime(2023, 9, 1, 16, 10, 57, 870, DateTimeKind.Local).AddTicks(3072), new DateTime(2023, 8, 1, 16, 10, 57, 870, DateTimeKind.Local).AddTicks(3071) });
        }
    }
}
