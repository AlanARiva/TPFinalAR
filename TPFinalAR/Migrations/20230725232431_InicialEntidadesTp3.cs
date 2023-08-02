using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPFinalAR.Migrations
{
    /// <inheritdoc />
    public partial class InicialEntidadesTp3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Caja_ahorro",
                columns: table => new
                {
                    _id_caja = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    _cbu = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    _saldo = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caja_ahorro", x => x._id_caja);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    _id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    _dni = table.Column<int>(type: "int", nullable: false),
                    _nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    _apellido = table.Column<string>(type: "varchar(50)", nullable: false),
                    _mail = table.Column<string>(type: "varchar(512)", nullable: false),
                    _password = table.Column<string>(type: "varchar(50)", nullable: false),
                    _intentosFallidos = table.Column<int>(type: "int", nullable: false),
                    _esUsuarioAdmin = table.Column<bool>(type: "bit", nullable: false),
                    _bloqueado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x._id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "Movimiento",
                columns: table => new
                {
                    _id_Movimiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    _id_CajaDeAhorro = table.Column<int>(type: "int", nullable: false),
                    _detalle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _monto = table.Column<double>(type: "float", nullable: false),
                    _fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimiento", x => x._id_Movimiento);
                    table.ForeignKey(
                        name: "FK_Movimiento_Caja_ahorro__id_CajaDeAhorro",
                        column: x => x._id_CajaDeAhorro,
                        principalTable: "Caja_ahorro",
                        principalColumn: "_id_caja",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pago",
                columns: table => new
                {
                    _id_pago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    _id_usuario = table.Column<int>(type: "int", nullable: false),
                    _monto = table.Column<double>(type: "float", nullable: false),
                    _pagado = table.Column<bool>(type: "bit", nullable: false),
                    _metodo = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    _detalle = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    _id_metodo = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pago", x => x._id_pago);
                    table.ForeignKey(
                        name: "FK_Pago_Usuario__id_usuario",
                        column: x => x._id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "_id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plazo_fijo",
                columns: table => new
                {
                    _id_plazoFijo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    _id_usuario = table.Column<int>(type: "int", nullable: false),
                    _monto = table.Column<double>(type: "float", nullable: false),
                    _fechaIni = table.Column<DateTime>(type: "datetime", nullable: false),
                    _fechaFin = table.Column<DateTime>(type: "datetime", nullable: false),
                    _tasa = table.Column<double>(type: "float", nullable: false),
                    _pagado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plazo_fijo", x => x._id_plazoFijo);
                    table.ForeignKey(
                        name: "FK_Plazo_fijo_Usuario__id_usuario",
                        column: x => x._id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "_id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tarjeta_credito",
                columns: table => new
                {
                    _id_tarjeta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    _id_usuario = table.Column<int>(type: "int", nullable: false),
                    _numero = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    _codigoV = table.Column<int>(type: "int", nullable: false),
                    _limite = table.Column<double>(type: "float", nullable: false),
                    _consumos = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarjeta_credito", x => x._id_tarjeta);
                    table.ForeignKey(
                        name: "FK_Tarjeta_credito_Usuario__id_usuario",
                        column: x => x._id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "_id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioCajaDeAhorro",
                columns: table => new
                {
                    id_caja = table.Column<int>(type: "int", nullable: false),
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioCajaDeAhorro", x => new { x.id_caja, x.id_usuario });
                    table.ForeignKey(
                        name: "FK_UsuarioCajaDeAhorro_Caja_ahorro_id_caja",
                        column: x => x.id_caja,
                        principalTable: "Caja_ahorro",
                        principalColumn: "_id_caja",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioCajaDeAhorro_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "_id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movimiento__id_CajaDeAhorro",
                table: "Movimiento",
                column: "_id_CajaDeAhorro");

            migrationBuilder.CreateIndex(
                name: "IX_Pago__id_usuario",
                table: "Pago",
                column: "_id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Plazo_fijo__id_usuario",
                table: "Plazo_fijo",
                column: "_id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Tarjeta_credito__id_usuario",
                table: "Tarjeta_credito",
                column: "_id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioCajaDeAhorro_id_usuario",
                table: "UsuarioCajaDeAhorro",
                column: "id_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movimiento");

            migrationBuilder.DropTable(
                name: "Pago");

            migrationBuilder.DropTable(
                name: "Plazo_fijo");

            migrationBuilder.DropTable(
                name: "Tarjeta_credito");

            migrationBuilder.DropTable(
                name: "UsuarioCajaDeAhorro");

            migrationBuilder.DropTable(
                name: "Caja_ahorro");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
