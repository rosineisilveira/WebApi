using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class RestrictMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matricula_Aluno",
                table: "matricula");

            migrationBuilder.DropForeignKey(
                name: "FK_Matricula_Pagamento",
                table: "matricula");

            migrationBuilder.AddForeignKey(
                name: "FK_Matricula_Aluno",
                table: "matricula",
                column: "AlunoId",
                principalTable: "aluno",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matricula_Pagamento",
                table: "matricula",
                column: "PagamentoId",
                principalTable: "pagamento",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matricula_Aluno",
                table: "matricula");

            migrationBuilder.DropForeignKey(
                name: "FK_Matricula_Pagamento",
                table: "matricula");

            migrationBuilder.AddForeignKey(
                name: "FK_Matricula_Aluno",
                table: "matricula",
                column: "AlunoId",
                principalTable: "aluno",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matricula_Pagamento",
                table: "matricula",
                column: "PagamentoId",
                principalTable: "pagamento",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
