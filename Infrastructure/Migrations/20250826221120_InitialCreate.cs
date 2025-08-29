using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RutEmpresa = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    RazonSocial = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NombreComercial = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Giro = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DireccionCasaMatriz = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    ComunaCasaMatriz = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CiudadCasaMatriz = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RegionCasaMatriz = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SitioWeb = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CodigoSII = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ResolucionSII = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FechaResolucionSII = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MonedaPrincipalId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    FormatoFecha = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "dd/MM/yyyy"),
                    SeparadorDecimal = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false, defaultValue: ","),
                    PermiteInventarioNegativo = table.Column<bool>(type: "bit", nullable: false),
                    MetodoValorizacionId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ManejaLotes = table.Column<bool>(type: "bit", nullable: false),
                    ManejaSeries = table.Column<bool>(type: "bit", nullable: false),
                    ManejaUbicaciones = table.Column<bool>(type: "bit", nullable: false),
                    RequiereAutenticacionDosFactor = table.Column<bool>(type: "bit", nullable: false),
                    TiempoExpiracionSesion = table.Column<int>(type: "int", nullable: false, defaultValue: 480),
                    MaximoIntentosLogin = table.Column<int>(type: "int", nullable: false, defaultValue: 5),
                    TiempoBloqueoTemporalMinutos = table.Column<int>(type: "int", nullable: false, defaultValue: 30),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioCreacion = table.Column<int>(type: "int", nullable: false),
                    UsuarioModificacion = table.Column<int>(type: "int", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                    table.CheckConstraint("CK_Empresas_MaximoIntentos", "[MaximoIntentosLogin] BETWEEN 3 AND 10");
                    table.CheckConstraint("CK_Empresas_SeparadorDecimal", "[SeparadorDecimal] IN (',', '.')");
                    table.CheckConstraint("CK_Empresas_TiempoBloqueo", "[TiempoBloqueoTemporalMinutos] > 0");
                    table.CheckConstraint("CK_Empresas_TiempoExpiracion", "[TiempoExpiracionSesion] BETWEEN 30 AND 1440");
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Modulo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EsPermisoSistema = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EsRolSistema = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    EsAdministrador = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PuedeGestionarUsuarios = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PuedeGestionarRoles = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PuedeVerTodosLosDatos = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioCreacion = table.Column<int>(type: "int", nullable: false),
                    UsuarioModificacion = table.Column<int>(type: "int", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    EmpresaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sucursales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioCreacion = table.Column<int>(type: "int", nullable: false),
                    UsuarioModificacion = table.Column<int>(type: "int", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    EmpresaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sucursales_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RutPersona = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    RequiereCambioPassword = table.Column<bool>(type: "bit", nullable: false),
                    FechaExpiracionPassword = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IntentosLoginFallidos = table.Column<int>(type: "int", nullable: false),
                    FechaUltimoLoginFallido = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CuentaBloqueada = table.Column<bool>(type: "bit", nullable: false),
                    FechaBloqueoCuenta = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AutenticacionDobleFactorHabilitada = table.Column<bool>(type: "bit", nullable: false),
                    SecretoTOTP = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CodigosRecuperacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaUltimoLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TokenRecuperacion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FechaExpiracionTokenRecuperacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TokenVerificacionEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EmailVerificado = table.Column<bool>(type: "bit", nullable: false),
                    Idioma = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValue: "es-CL"),
                    ZonaHoraria = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "America/Santiago"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioCreacion = table.Column<int>(type: "int", nullable: false),
                    UsuarioModificacion = table.Column<int>(type: "int", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    EmpresaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.CheckConstraint("CK_Usuarios_Idioma", "[Idioma] IN ('es-CL', 'es-ES', 'en-US')");
                    table.CheckConstraint("CK_Usuarios_IntentosLogin", "[IntentosLoginFallidos] >= 0 AND [IntentosLoginFallidos] <= 10");
                    table.ForeignKey(
                        name: "FK_Usuarios_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolesPermisos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolId = table.Column<int>(type: "int", nullable: false),
                    PermisoId = table.Column<int>(type: "int", nullable: false),
                    Concedido = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaAsignacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UsuarioAsignacion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesPermisos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolesPermisos_Permisos_PermisoId",
                        column: x => x.PermisoId,
                        principalTable: "Permisos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolesPermisos_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bodegas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SucursalId = table.Column<int>(type: "int", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bodegas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bodegas_Sucursales_SucursalId",
                        column: x => x.SucursalId,
                        principalTable: "Sucursales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditoriaAccesos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TipoEvento = table.Column<int>(type: "int", nullable: false),
                    FechaEvento = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DireccionIP = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    UserAgent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Exitoso = table.Column<bool>(type: "bit", nullable: false),
                    MensajeError = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    DatosAdicionales = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditoriaAccesos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditoriaAccesos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SesionesUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    TokenSesion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FechaInicioSesion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    FechaExpiracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaUltimaActividad = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DireccionIP = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    UserAgent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Dispositivo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SistemaOperativo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Navegador = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SesionActiva = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaCierre = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RazonCierre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SesionesUsuario", x => x.Id);
                    table.CheckConstraint("CK_SesionesUsuario_FechasValidas", "[FechaExpiracion] > [FechaInicioSesion]");
                    table.CheckConstraint("CK_SesionesUsuario_RazonCierre", "([SesionActiva] = 1 AND [FechaCierre] IS NULL AND [RazonCierre] IS NULL) OR ([SesionActiva] = 0 AND [FechaCierre] IS NOT NULL AND [RazonCierre] IS NOT NULL)");
                    table.CheckConstraint("CK_SesionesUsuario_RazonCierreValores", "[RazonCierre] IS NULL OR [RazonCierre] IN ('LOGOUT', 'TIMEOUT', 'FORCE_CLOSE', 'EXPIRED')");
                    table.ForeignKey(
                        name: "FK_SesionesUsuario_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false),
                    FechaAsignacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SucursalId = table.Column<int>(type: "int", nullable: true),
                    BodegaId = table.Column<int>(type: "int", nullable: true),
                    UsuarioAsignacion = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosRoles", x => x.Id);
                    table.CheckConstraint("CK_UsuariosRoles_VencimientoValido", "[FechaVencimiento] IS NULL OR [FechaVencimiento] > [FechaAsignacion]");
                    table.ForeignKey(
                        name: "FK_UsuariosRoles_Bodegas_BodegaId",
                        column: x => x.BodegaId,
                        principalTable: "Bodegas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuariosRoles_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosRoles_Sucursales_SucursalId",
                        column: x => x.SucursalId,
                        principalTable: "Sucursales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuariosRoles_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditoriaAccesos_DireccionIP",
                table: "AuditoriaAccesos",
                column: "DireccionIP");

            migrationBuilder.CreateIndex(
                name: "IX_AuditoriaAccesos_Exitoso",
                table: "AuditoriaAccesos",
                column: "Exitoso");

            migrationBuilder.CreateIndex(
                name: "IX_AuditoriaAccesos_FechaEvento",
                table: "AuditoriaAccesos",
                column: "FechaEvento");

            migrationBuilder.CreateIndex(
                name: "IX_AuditoriaAccesos_FechaTipoExitoso",
                table: "AuditoriaAccesos",
                columns: new[] { "FechaEvento", "TipoEvento", "Exitoso" });

            migrationBuilder.CreateIndex(
                name: "IX_AuditoriaAccesos_TipoEvento",
                table: "AuditoriaAccesos",
                column: "TipoEvento");

            migrationBuilder.CreateIndex(
                name: "IX_AuditoriaAccesos_UsuarioId",
                table: "AuditoriaAccesos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Bodegas_SucursalId",
                table: "Bodegas",
                column: "SucursalId");

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_Activo",
                table: "Empresas",
                column: "Activo");

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_FechaCreacion",
                table: "Empresas",
                column: "FechaCreacion");

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_RazonSocial",
                table: "Empresas",
                column: "RazonSocial");

            migrationBuilder.CreateIndex(
                name: "UK_Empresas_RutEmpresa",
                table: "Empresas",
                column: "RutEmpresa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_Activo",
                table: "Permisos",
                column: "Activo");

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_Categoria",
                table: "Permisos",
                column: "Categoria");

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_Modulo",
                table: "Permisos",
                column: "Modulo");

            migrationBuilder.CreateIndex(
                name: "UK_Permisos_Codigo",
                table: "Permisos",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Activo",
                table: "Roles",
                column: "Activo");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_EmpresaId",
                table: "Roles",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_EsRolSistema",
                table: "Roles",
                column: "EsRolSistema",
                filter: "[EsRolSistema] = 1");

            migrationBuilder.CreateIndex(
                name: "UK_Roles_EmpresaNombre",
                table: "Roles",
                columns: new[] { "EmpresaId", "Nombre" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolesPermisos_PermisoId",
                table: "RolesPermisos",
                column: "PermisoId");

            migrationBuilder.CreateIndex(
                name: "IX_RolesPermisos_RolId",
                table: "RolesPermisos",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "UK_RolesPermisos_RolPermiso",
                table: "RolesPermisos",
                columns: new[] { "RolId", "PermisoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SesionesUsuario_DireccionIP",
                table: "SesionesUsuario",
                column: "DireccionIP");

            migrationBuilder.CreateIndex(
                name: "IX_SesionesUsuario_FechaExpiracion",
                table: "SesionesUsuario",
                column: "FechaExpiracion");

            migrationBuilder.CreateIndex(
                name: "IX_SesionesUsuario_SesionActiva",
                table: "SesionesUsuario",
                column: "SesionActiva");

            migrationBuilder.CreateIndex(
                name: "IX_SesionesUsuario_UsuarioId",
                table: "SesionesUsuario",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "UK_SesionesUsuario_Token",
                table: "SesionesUsuario",
                column: "TokenSesion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sucursales_EmpresaId",
                table: "Sucursales",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Activo",
                table: "Usuarios",
                column: "Activo");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_CuentaBloqueada",
                table: "Usuarios",
                column: "CuentaBloqueada",
                filter: "[CuentaBloqueada] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_EmpresaId",
                table: "Usuarios",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_TokenRecuperacion",
                table: "Usuarios",
                column: "TokenRecuperacion",
                filter: "[TokenRecuperacion] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UK_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Usuarios_EmpresaNombreUsuario",
                table: "Usuarios",
                columns: new[] { "EmpresaId", "NombreUsuario" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRoles_Activo",
                table: "UsuariosRoles",
                column: "Activo");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRoles_BodegaId",
                table: "UsuariosRoles",
                column: "BodegaId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRoles_RolId",
                table: "UsuariosRoles",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRoles_SucursalId",
                table: "UsuariosRoles",
                column: "SucursalId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRoles_UsuarioId",
                table: "UsuariosRoles",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRoles_Vigencia",
                table: "UsuariosRoles",
                column: "FechaVencimiento",
                filter: "[FechaVencimiento] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UK_UsuariosRoles_UsuarioRolSucursalBodega",
                table: "UsuariosRoles",
                columns: new[] { "UsuarioId", "RolId", "SucursalId", "BodegaId" },
                unique: true,
                filter: "[SucursalId] IS NOT NULL AND [BodegaId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditoriaAccesos");

            migrationBuilder.DropTable(
                name: "RolesPermisos");

            migrationBuilder.DropTable(
                name: "SesionesUsuario");

            migrationBuilder.DropTable(
                name: "UsuariosRoles");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "Bodegas");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Sucursales");

            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
