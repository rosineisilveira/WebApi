using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class DataMatriculaMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TreinoId",
                table: "matricula");

            migrationBuilder.AddColumn<DateTime>(
                name: "data_cadastro",
                table: "matricula",
                type: "SMALLDATETIME",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "matricula",
                type: "INTEGER",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "data_cadastro",
                table: "matricula");

            migrationBuilder.DropColumn(
                name: "status",
                table: "matricula");

            migrationBuilder.AddColumn<int>(
                name: "TreinoId",
                table: "matricula",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
