using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class errorMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "treino_matricula");

            migrationBuilder.CreateTable(
                name: "matricula_treino",
                columns: table => new
                {
                    matricula_id = table.Column<int>(type: "INTEGER", nullable: false),
                    treino_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matricula_treino", x => new { x.matricula_id, x.treino_id });
                    table.ForeignKey(
                        name: "FK_matricula_treino_matricula_id",
                        column: x => x.matricula_id,
                        principalTable: "matricula",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_matricula_treino_treino_id",
                        column: x => x.treino_id,
                        principalTable: "treino",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_matricula_treino_treino_id",
                table: "matricula_treino",
                column: "treino_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "matricula_treino");

            migrationBuilder.CreateTable(
                name: "treino_matricula",
                columns: table => new
                {
                    matricula_id = table.Column<int>(type: "INTEGER", nullable: false),
                    treino_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_treino_matricula", x => new { x.matricula_id, x.treino_id });
                    table.ForeignKey(
                        name: "FK_treino_matricula_matricula_id",
                        column: x => x.matricula_id,
                        principalTable: "matricula",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_treino_matricula_treino_id",
                        column: x => x.treino_id,
                        principalTable: "treino",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_treino_matricula_treino_id",
                table: "treino_matricula",
                column: "treino_id");
        }
    }
}
