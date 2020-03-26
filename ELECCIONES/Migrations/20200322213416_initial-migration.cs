using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ELECCIONES.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ciudadanos",
                columns: table => new
                {
                    id_Ciudadanos = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cedula = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    Apellido = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    Email = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ciudadan__A7ACDF178EAA5AA0", x => x.id_Ciudadanos);
                });

            migrationBuilder.CreateTable(
                name: "Partidos",
                columns: table => new
                {
                    id_Partidos = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    Descripcion = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Logo_Partido = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Partidos__189159DD5031CFB1", x => x.id_Partidos);
                });

            migrationBuilder.CreateTable(
                name: "Puesto_Electo",
                columns: table => new
                {
                    id_PuestoE = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    Descripcion = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Puesto_E__46878D972E299F5C", x => x.id_PuestoE);
                });

            migrationBuilder.CreateTable(
                name: "Candidatos",
                columns: table => new
                {
                    id_Candidatos = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    Apellido = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    Partido_Pertenece = table.Column<int>(nullable: false),
                    Puesto_Aspira = table.Column<int>(nullable: false),
                    Foto_Perfil = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Estado = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Candidat__FFAB99880628D312", x => x.id_Candidatos);
                    table.ForeignKey(
                        name: "FK_PartidosPe",
                        column: x => x.Partido_Pertenece,
                        principalTable: "Partidos",
                        principalColumn: "id_Partidos",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PuestoAs",
                        column: x => x.Puesto_Aspira,
                        principalTable: "Puesto_Electo",
                        principalColumn: "id_PuestoE",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Elecciones",
                columns: table => new
                {
                    id_Elecciones = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    Fecha_Realizacion = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    Estado = table.Column<bool>(nullable: false),
                    idCandidatos = table.Column<int>(nullable: false),
                    idCiudadanos = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Eleccion__A4B502946D8F97B4", x => x.id_Elecciones);
                    table.ForeignKey(
                        name: "FK_Candidatos",
                        column: x => x.idCandidatos,
                        principalTable: "Candidatos",
                        principalColumn: "id_Candidatos",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ciudadanos",
                        column: x => x.idCiudadanos,
                        principalTable: "Ciudadanos",
                        principalColumn: "id_Ciudadanos",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Resultado",
                columns: table => new
                {
                    id_Resultado = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Resultado_Total = table.Column<int>(nullable: false),
                    idElecciones = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Resultad__F64F43EE2FB5B041", x => x.id_Resultado);
                    table.ForeignKey(
                        name: "FK_Elecciones",
                        column: x => x.idElecciones,
                        principalTable: "Elecciones",
                        principalColumn: "id_Elecciones",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidatos_Partido_Pertenece",
                table: "Candidatos",
                column: "Partido_Pertenece");

            migrationBuilder.CreateIndex(
                name: "IX_Candidatos_Puesto_Aspira",
                table: "Candidatos",
                column: "Puesto_Aspira");

            migrationBuilder.CreateIndex(
                name: "IX_Elecciones_idCandidatos",
                table: "Elecciones",
                column: "idCandidatos");

            migrationBuilder.CreateIndex(
                name: "IX_Elecciones_idCiudadanos",
                table: "Elecciones",
                column: "idCiudadanos");

            migrationBuilder.CreateIndex(
                name: "IX_Resultado_idElecciones",
                table: "Resultado",
                column: "idElecciones");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resultado");

            migrationBuilder.DropTable(
                name: "Elecciones");

            migrationBuilder.DropTable(
                name: "Candidatos");

            migrationBuilder.DropTable(
                name: "Ciudadanos");

            migrationBuilder.DropTable(
                name: "Partidos");

            migrationBuilder.DropTable(
                name: "Puesto_Electo");
        }
    }
}
