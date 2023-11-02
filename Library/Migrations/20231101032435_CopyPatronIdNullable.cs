using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    public partial class CopyPatronIdNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Copies_Patrons_PatronId",
                table: "Copies");

            migrationBuilder.AlterColumn<int>(
                name: "PatronId",
                table: "Copies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Copies_Patrons_PatronId",
                table: "Copies",
                column: "PatronId",
                principalTable: "Patrons",
                principalColumn: "PatronId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Copies_Patrons_PatronId",
                table: "Copies");

            migrationBuilder.AlterColumn<int>(
                name: "PatronId",
                table: "Copies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Copies_Patrons_PatronId",
                table: "Copies",
                column: "PatronId",
                principalTable: "Patrons",
                principalColumn: "PatronId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
