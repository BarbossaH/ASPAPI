using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPAPI.Migrations
{
    /// <inheritdoc />
    public partial class entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "Refreshtoken_pkey",
                table: "Refreshtoken");

            migrationBuilder.RenameTable(
                name: "Refreshtoken",
                newName: "RefreshToken");

            migrationBuilder.AlterColumn<string>(
                name: "role",
                table: "Users",
                type: "character varying(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "RefreshToken_pkey",
                table: "RefreshToken",
                column: "userid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "RefreshToken_pkey",
                table: "RefreshToken");

            migrationBuilder.RenameTable(
                name: "RefreshToken",
                newName: "Refreshtoken");

            migrationBuilder.AlterColumn<string>(
                name: "role",
                table: "Users",
                type: "character varying(50)",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "Refreshtoken_pkey",
                table: "Refreshtoken",
                column: "userid");
        }
    }
}
