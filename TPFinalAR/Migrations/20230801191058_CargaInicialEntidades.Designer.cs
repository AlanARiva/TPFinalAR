﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TPFinalAR.Models;

#nullable disable

namespace TPFinalAR.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20230801191058_CargaInicialEntidades")]
    partial class CargaInicialEntidades
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TPFinalAR.Models.CajaDeAhorro", b =>
                {
                    b.Property<int>("_id_caja")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("_id_caja"));

                    b.Property<string>("_cbu")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<double>("_saldo")
                        .HasColumnType("float");

                    b.HasKey("_id_caja");

                    b.ToTable("Caja_ahorro", (string)null);

                    b.HasData(
                        new
                        {
                            _id_caja = 1,
                            _cbu = "11120221121",
                            _saldo = 0.0
                        },
                        new
                        {
                            _id_caja = 2,
                            _cbu = "22220221122",
                            _saldo = 0.0
                        },
                        new
                        {
                            _id_caja = 3,
                            _cbu = "33320221123",
                            _saldo = 0.0
                        },
                        new
                        {
                            _id_caja = 4,
                            _cbu = "44420221124",
                            _saldo = 0.0
                        },
                        new
                        {
                            _id_caja = 5,
                            _cbu = "55520221125",
                            _saldo = 0.0
                        },
                        new
                        {
                            _id_caja = 6,
                            _cbu = "66620221125",
                            _saldo = 0.0
                        });
                });

            modelBuilder.Entity("TPFinalAR.Models.Movimiento", b =>
                {
                    b.Property<int>("_id_Movimiento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("_id_Movimiento"));

                    b.Property<string>("_detalle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("_fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("_id_CajaDeAhorro")
                        .HasColumnType("int");

                    b.Property<double>("_monto")
                        .HasColumnType("float");

                    b.HasKey("_id_Movimiento");

                    b.HasIndex("_id_CajaDeAhorro");

                    b.ToTable("Movimiento", (string)null);
                });

            modelBuilder.Entity("TPFinalAR.Models.Pago", b =>
                {
                    b.Property<int>("_id_pago")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("_id_pago"));

                    b.Property<string>("_detalle")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<long>("_id_metodo")
                        .HasColumnType("bigint");

                    b.Property<int>("_id_usuario")
                        .HasColumnType("int");

                    b.Property<string>("_metodo")
                        .HasColumnType("nvarchar(200)");

                    b.Property<double>("_monto")
                        .HasColumnType("float");

                    b.Property<bool>("_pagado")
                        .HasColumnType("bit");

                    b.HasKey("_id_pago");

                    b.HasIndex("_id_usuario");

                    b.ToTable("Pago", (string)null);
                });

            modelBuilder.Entity("TPFinalAR.Models.PlazoFijo", b =>
                {
                    b.Property<int>("_id_plazoFijo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("_id_plazoFijo"));

                    b.Property<DateTime>("_fechaFin")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("_fechaIni")
                        .HasColumnType("datetime");

                    b.Property<int>("_id_usuario")
                        .HasColumnType("int");

                    b.Property<double>("_monto")
                        .HasColumnType("float");

                    b.Property<bool>("_pagado")
                        .HasColumnType("bit");

                    b.Property<double>("_tasa")
                        .HasColumnType("float");

                    b.HasKey("_id_plazoFijo");

                    b.HasIndex("_id_usuario");

                    b.ToTable("Plazo_fijo", (string)null);

                    b.HasData(
                        new
                        {
                            _id_plazoFijo = 1,
                            _fechaFin = new DateTime(2023, 9, 1, 16, 10, 57, 870, DateTimeKind.Local).AddTicks(3055),
                            _fechaIni = new DateTime(2023, 8, 1, 16, 10, 57, 870, DateTimeKind.Local).AddTicks(3040),
                            _id_usuario = 1,
                            _monto = 1000.0,
                            _pagado = false,
                            _tasa = 1.5
                        },
                        new
                        {
                            _id_plazoFijo = 2,
                            _fechaFin = new DateTime(2023, 9, 1, 16, 10, 57, 870, DateTimeKind.Local).AddTicks(3066),
                            _fechaIni = new DateTime(2023, 8, 1, 16, 10, 57, 870, DateTimeKind.Local).AddTicks(3066),
                            _id_usuario = 2,
                            _monto = 2000.0,
                            _pagado = false,
                            _tasa = 1.5
                        },
                        new
                        {
                            _id_plazoFijo = 3,
                            _fechaFin = new DateTime(2023, 9, 1, 16, 10, 57, 870, DateTimeKind.Local).AddTicks(3070),
                            _fechaIni = new DateTime(2023, 8, 1, 16, 10, 57, 870, DateTimeKind.Local).AddTicks(3069),
                            _id_usuario = 3,
                            _monto = 3000.0,
                            _pagado = false,
                            _tasa = 1.5
                        },
                        new
                        {
                            _id_plazoFijo = 4,
                            _fechaFin = new DateTime(2023, 9, 1, 16, 10, 57, 870, DateTimeKind.Local).AddTicks(3072),
                            _fechaIni = new DateTime(2023, 8, 1, 16, 10, 57, 870, DateTimeKind.Local).AddTicks(3071),
                            _id_usuario = 4,
                            _monto = 4000.0,
                            _pagado = false,
                            _tasa = 1.5
                        });
                });

            modelBuilder.Entity("TPFinalAR.Models.TarjetaDeCredito", b =>
                {
                    b.Property<int>("_id_tarjeta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("_id_tarjeta"));

                    b.Property<int>("_codigoV")
                        .HasColumnType("int");

                    b.Property<double>("_consumos")
                        .HasColumnType("float");

                    b.Property<int>("_id_usuario")
                        .HasColumnType("int");

                    b.Property<double>("_limite")
                        .HasColumnType("float");

                    b.Property<string>("_numero")
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("_id_tarjeta");

                    b.HasIndex("_id_usuario");

                    b.ToTable("Tarjeta_credito", (string)null);

                    b.HasData(
                        new
                        {
                            _id_tarjeta = 1,
                            _codigoV = 0,
                            _consumos = 100.0,
                            _id_usuario = 1,
                            _limite = 500000.0,
                            _numero = "11120221121"
                        },
                        new
                        {
                            _id_tarjeta = 2,
                            _codigoV = 0,
                            _consumos = 900.0,
                            _id_usuario = 2,
                            _limite = 400000.0,
                            _numero = "22220221121"
                        },
                        new
                        {
                            _id_tarjeta = 3,
                            _codigoV = 0,
                            _consumos = 400.0,
                            _id_usuario = 3,
                            _limite = 600000.0,
                            _numero = "33320221121"
                        },
                        new
                        {
                            _id_tarjeta = 4,
                            _codigoV = 0,
                            _consumos = 600.0,
                            _id_usuario = 4,
                            _limite = 200000.0,
                            _numero = "44420221121"
                        });
                });

            modelBuilder.Entity("TPFinalAR.Models.Usuario", b =>
                {
                    b.Property<int>("_id_usuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("_id_usuario"));

                    b.Property<string>("_apellido")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("_bloqueado")
                        .HasColumnType("bit");

                    b.Property<int>("_dni")
                        .HasColumnType("int");

                    b.Property<bool>("_esUsuarioAdmin")
                        .HasColumnType("bit");

                    b.Property<int>("_intentosFallidos")
                        .HasColumnType("int");

                    b.Property<string>("_mail")
                        .IsRequired()
                        .HasColumnType("varchar(512)");

                    b.Property<string>("_nombre")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("_password")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("_id_usuario");

                    b.ToTable("Usuario", (string)null);

                    b.HasData(
                        new
                        {
                            _id_usuario = 1,
                            _apellido = "GREGO",
                            _bloqueado = false,
                            _dni = 111,
                            _esUsuarioAdmin = false,
                            _intentosFallidos = 0,
                            _mail = "M@G",
                            _nombre = "MATIAS",
                            _password = "111"
                        },
                        new
                        {
                            _id_usuario = 2,
                            _apellido = "RIVA",
                            _bloqueado = false,
                            _dni = 222,
                            _esUsuarioAdmin = false,
                            _intentosFallidos = 0,
                            _mail = "A@R",
                            _nombre = "ALAN",
                            _password = "222"
                        },
                        new
                        {
                            _id_usuario = 3,
                            _apellido = "VILLEGAS",
                            _bloqueado = false,
                            _dni = 333,
                            _esUsuarioAdmin = false,
                            _intentosFallidos = 0,
                            _mail = "N@V",
                            _nombre = "NICOLAS",
                            _password = "333"
                        },
                        new
                        {
                            _id_usuario = 4,
                            _apellido = "GOMEZ",
                            _bloqueado = false,
                            _dni = 444,
                            _esUsuarioAdmin = true,
                            _intentosFallidos = 0,
                            _mail = "W@G",
                            _nombre = "WALTER",
                            _password = "444"
                        });
                });

            modelBuilder.Entity("TPFinalAR.Models.UsuarioCajaDeAhorro", b =>
                {
                    b.Property<int>("id_caja")
                        .HasColumnType("int");

                    b.Property<int>("id_usuario")
                        .HasColumnType("int");

                    b.HasKey("id_caja", "id_usuario");

                    b.HasIndex("id_usuario");

                    b.ToTable("UsuarioCajaDeAhorro");

                    b.HasData(
                        new
                        {
                            id_caja = 1,
                            id_usuario = 1
                        },
                        new
                        {
                            id_caja = 2,
                            id_usuario = 2
                        },
                        new
                        {
                            id_caja = 3,
                            id_usuario = 3
                        },
                        new
                        {
                            id_caja = 4,
                            id_usuario = 4
                        },
                        new
                        {
                            id_caja = 5,
                            id_usuario = 1
                        },
                        new
                        {
                            id_caja = 6,
                            id_usuario = 1
                        });
                });

            modelBuilder.Entity("TPFinalAR.Models.Movimiento", b =>
                {
                    b.HasOne("TPFinalAR.Models.CajaDeAhorro", "_cajaDeAhorro")
                        .WithMany("_movimientos")
                        .HasForeignKey("_id_CajaDeAhorro")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("_cajaDeAhorro");
                });

            modelBuilder.Entity("TPFinalAR.Models.Pago", b =>
                {
                    b.HasOne("TPFinalAR.Models.Usuario", "_usuario")
                        .WithMany("_pagos")
                        .HasForeignKey("_id_usuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("_usuario");
                });

            modelBuilder.Entity("TPFinalAR.Models.PlazoFijo", b =>
                {
                    b.HasOne("TPFinalAR.Models.Usuario", "_titular")
                        .WithMany("_plazosFijos")
                        .HasForeignKey("_id_usuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("_titular");
                });

            modelBuilder.Entity("TPFinalAR.Models.TarjetaDeCredito", b =>
                {
                    b.HasOne("TPFinalAR.Models.Usuario", "_titular")
                        .WithMany("_tarjetas")
                        .HasForeignKey("_id_usuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("_titular");
                });

            modelBuilder.Entity("TPFinalAR.Models.UsuarioCajaDeAhorro", b =>
                {
                    b.HasOne("TPFinalAR.Models.CajaDeAhorro", "caja")
                        .WithMany("usuarioCajas")
                        .HasForeignKey("id_caja")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TPFinalAR.Models.Usuario", "user")
                        .WithMany("usuarioCajas")
                        .HasForeignKey("id_usuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("caja");

                    b.Navigation("user");
                });

            modelBuilder.Entity("TPFinalAR.Models.CajaDeAhorro", b =>
                {
                    b.Navigation("_movimientos");

                    b.Navigation("usuarioCajas");
                });

            modelBuilder.Entity("TPFinalAR.Models.Usuario", b =>
                {
                    b.Navigation("_pagos");

                    b.Navigation("_plazosFijos");

                    b.Navigation("_tarjetas");

                    b.Navigation("usuarioCajas");
                });
#pragma warning restore 612, 618
        }
    }
}