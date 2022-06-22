using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class karta_setPolazisteDolazisteToNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Karta_Stajalista_DolazisteID",
                table: "Karta");

            migrationBuilder.DropForeignKey(
                name: "FK_Karta_Stajalista_PolazisteID",
                table: "Karta");

            migrationBuilder.AlterColumn<int>(
                name: "PolazisteID",
                table: "Karta",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DolazisteID",
                table: "Karta",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Karta_Stajalista_DolazisteID",
                table: "Karta",
                column: "DolazisteID",
                principalTable: "Stajalista",
                principalColumn: "StajalistaID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Karta_Stajalista_PolazisteID",
                table: "Karta",
                column: "PolazisteID",
                principalTable: "Stajalista",
                principalColumn: "StajalistaID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Karta_Stajalista_DolazisteID",
                table: "Karta");

            migrationBuilder.DropForeignKey(
                name: "FK_Karta_Stajalista_PolazisteID",
                table: "Karta");

            migrationBuilder.AlterColumn<int>(
                name: "PolazisteID",
                table: "Karta",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DolazisteID",
                table: "Karta",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Karta_Stajalista_DolazisteID",
                table: "Karta",
                column: "DolazisteID",
                principalTable: "Stajalista",
                principalColumn: "StajalistaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Karta_Stajalista_PolazisteID",
                table: "Karta",
                column: "PolazisteID",
                principalTable: "Stajalista",
                principalColumn: "StajalistaID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
