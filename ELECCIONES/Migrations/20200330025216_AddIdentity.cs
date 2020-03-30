using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ELECCIONES.Migrations
{
    public partial class AddIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidatos",
                table: "Elecciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Ciudadanos",
                table: "Elecciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Elecciones",
                table: "Resultado");

            migrationBuilder.DropIndex(
                name: "IX_Elecciones_idCandidatos",
                table: "Elecciones");

            migrationBuilder.DropIndex(
                name: "IX_Elecciones_idCiudadanos",
                table: "Elecciones");

            migrationBuilder.DropColumn(
                name: "Resultado_Total",
                table: "Resultado");

            migrationBuilder.DropColumn(
                name: "idCandidatos",
                table: "Elecciones");

            migrationBuilder.DropColumn(
                name: "idCiudadanos",
                table: "Elecciones");

            migrationBuilder.AlterColumn<int>(
                name: "idElecciones",
                table: "Resultado",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "idCandidatos",
                table: "Resultado",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "idCiudadanos",
                table: "Resultado",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Logo_Partido",
                table: "Partidos",
                unicode: false,
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Fecha_Realizacion",
                table: "Elecciones",
                type: "date",
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<long>(
                name: "Cedula",
                table: "Ciudadanos",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Foto_Perfil",
                table: "Candidatos",
                unicode: false,
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resultado_idCandidatos",
                table: "Resultado",
                column: "idCandidatos");

            migrationBuilder.CreateIndex(
                name: "IX_Resultado_idCiudadanos",
                table: "Resultado",
                column: "idCiudadanos");

            migrationBuilder.AddForeignKey(
                name: "FK_Resultado_Candidatos",
                table: "Resultado",
                column: "idCandidatos",
                principalTable: "Candidatos",
                principalColumn: "id_Candidatos",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Resultado_Ciudadanos",
                table: "Resultado",
                column: "idCiudadanos",
                principalTable: "Ciudadanos",
                principalColumn: "id_Ciudadanos",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Resultado_Elecciones",
                table: "Resultado",
                column: "idElecciones",
                principalTable: "Elecciones",
                principalColumn: "id_Elecciones",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resultado_Candidatos",
                table: "Resultado");

            migrationBuilder.DropForeignKey(
                name: "FK_Resultado_Ciudadanos",
                table: "Resultado");

            migrationBuilder.DropForeignKey(
                name: "FK_Resultado_Elecciones",
                table: "Resultado");

            migrationBuilder.DropIndex(
                name: "IX_Resultado_idCandidatos",
                table: "Resultado");

            migrationBuilder.DropIndex(
                name: "IX_Resultado_idCiudadanos",
                table: "Resultado");

            migrationBuilder.DropColumn(
                name: "idCandidatos",
                table: "Resultado");

            migrationBuilder.DropColumn(
                name: "idCiudadanos",
                table: "Resultado");

            migrationBuilder.AlterColumn<int>(
                name: "idElecciones",
                table: "Resultado",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Resultado_Total",
                table: "Resultado",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Logo_Partido",
                table: "Partidos",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Fecha_Realizacion",
                table: "Elecciones",
                unicode: false,
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "idCandidatos",
                table: "Elecciones",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idCiudadanos",
                table: "Elecciones",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Cedula",
                table: "Ciudadanos",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<string>(
                name: "Foto_Perfil",
                table: "Candidatos",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Elecciones_idCandidatos",
                table: "Elecciones",
                column: "idCandidatos");

            migrationBuilder.CreateIndex(
                name: "IX_Elecciones_idCiudadanos",
                table: "Elecciones",
                column: "idCiudadanos");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidatos",
                table: "Elecciones",
                column: "idCandidatos",
                principalTable: "Candidatos",
                principalColumn: "id_Candidatos",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ciudadanos",
                table: "Elecciones",
                column: "idCiudadanos",
                principalTable: "Ciudadanos",
                principalColumn: "id_Ciudadanos",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Elecciones",
                table: "Resultado",
                column: "idElecciones",
                principalTable: "Elecciones",
                principalColumn: "id_Elecciones",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
