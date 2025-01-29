using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthServer.Migrations
{
    /// <inheritdoc />
    public partial class renomeandotabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medico_AspNetUsers_Id",
                table: "Medico");

            migrationBuilder.DropForeignKey(
                name: "FK_Paciente_AspNetUsers_Id",
                table: "Paciente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Paciente",
                table: "Paciente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Medico",
                table: "Medico");

            migrationBuilder.RenameTable(
                name: "Paciente",
                newName: "Pacientes");

            migrationBuilder.RenameTable(
                name: "Medico",
                newName: "Medicos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pacientes",
                table: "Pacientes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medicos",
                table: "Medicos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicos_AspNetUsers_Id",
                table: "Medicos",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_AspNetUsers_Id",
                table: "Pacientes",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicos_AspNetUsers_Id",
                table: "Medicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_AspNetUsers_Id",
                table: "Pacientes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pacientes",
                table: "Pacientes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Medicos",
                table: "Medicos");

            migrationBuilder.RenameTable(
                name: "Pacientes",
                newName: "Paciente");

            migrationBuilder.RenameTable(
                name: "Medicos",
                newName: "Medico");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Paciente",
                table: "Paciente",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medico",
                table: "Medico",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Medico_AspNetUsers_Id",
                table: "Medico",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Paciente_AspNetUsers_Id",
                table: "Paciente",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
