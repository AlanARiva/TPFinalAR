using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPFinalAR.Migrations
{
    /// <inheritdoc />
    public partial class ListaPlazosFijosEnCaja2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Plazo_fijo",
                keyColumn: "_id_plazoFijo",
                keyValue: 1,
                columns: new[] { "_fechaFin", "_fechaIni" },
                values: new object[] { new DateTime(2023, 9, 2, 16, 5, 30, 91, DateTimeKind.Local).AddTicks(7677), new DateTime(2023, 8, 2, 16, 5, 30, 91, DateTimeKind.Local).AddTicks(7653) });

            migrationBuilder.UpdateData(
                table: "Plazo_fijo",
                keyColumn: "_id_plazoFijo",
                keyValue: 2,
                columns: new[] { "_fechaFin", "_fechaIni" },
                values: new object[] { new DateTime(2023, 9, 2, 16, 5, 30, 91, DateTimeKind.Local).AddTicks(7696), new DateTime(2023, 8, 2, 16, 5, 30, 91, DateTimeKind.Local).AddTicks(7696) });

            migrationBuilder.UpdateData(
                table: "Plazo_fijo",
                keyColumn: "_id_plazoFijo",
                keyValue: 3,
                columns: new[] { "_fechaFin", "_fechaIni" },
                values: new object[] { new DateTime(2023, 9, 2, 16, 5, 30, 91, DateTimeKind.Local).AddTicks(7700), new DateTime(2023, 8, 2, 16, 5, 30, 91, DateTimeKind.Local).AddTicks(7699) });

            migrationBuilder.UpdateData(
                table: "Plazo_fijo",
                keyColumn: "_id_plazoFijo",
                keyValue: 4,
                columns: new[] { "_fechaFin", "_fechaIni" },
                values: new object[] { new DateTime(2023, 9, 2, 16, 5, 30, 91, DateTimeKind.Local).AddTicks(7702), new DateTime(2023, 8, 2, 16, 5, 30, 91, DateTimeKind.Local).AddTicks(7701) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Plazo_fijo",
                keyColumn: "_id_plazoFijo",
                keyValue: 1,
                columns: new[] { "_fechaFin", "_fechaIni" },
                values: new object[] { new DateTime(2023, 9, 2, 15, 57, 40, 204, DateTimeKind.Local).AddTicks(6857), new DateTime(2023, 8, 2, 15, 57, 40, 204, DateTimeKind.Local).AddTicks(6835) });

            migrationBuilder.UpdateData(
                table: "Plazo_fijo",
                keyColumn: "_id_plazoFijo",
                keyValue: 2,
                columns: new[] { "_fechaFin", "_fechaIni" },
                values: new object[] { new DateTime(2023, 9, 2, 15, 57, 40, 204, DateTimeKind.Local).AddTicks(6866), new DateTime(2023, 8, 2, 15, 57, 40, 204, DateTimeKind.Local).AddTicks(6866) });

            migrationBuilder.UpdateData(
                table: "Plazo_fijo",
                keyColumn: "_id_plazoFijo",
                keyValue: 3,
                columns: new[] { "_fechaFin", "_fechaIni" },
                values: new object[] { new DateTime(2023, 9, 2, 15, 57, 40, 204, DateTimeKind.Local).AddTicks(6869), new DateTime(2023, 8, 2, 15, 57, 40, 204, DateTimeKind.Local).AddTicks(6868) });

            migrationBuilder.UpdateData(
                table: "Plazo_fijo",
                keyColumn: "_id_plazoFijo",
                keyValue: 4,
                columns: new[] { "_fechaFin", "_fechaIni" },
                values: new object[] { new DateTime(2023, 9, 2, 15, 57, 40, 204, DateTimeKind.Local).AddTicks(6870), new DateTime(2023, 8, 2, 15, 57, 40, 204, DateTimeKind.Local).AddTicks(6870) });
        }
    }
}
